using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GraphClient _graphClient;

        public GameRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public GameRepository()
            : this(GraphClientFactory.Create()){}

        public bool SaveOrUpdate(Game game)
        {
            var gameWithoutTeams = game as GameWithoutTeams;

            if (!GameBaseNodeExists())
                CreateGameBaseNode();

            var gameBaseNode = _graphClient.GameBaseNode().Single();

            var gameNode = _graphClient.Create(game);

            _graphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = _graphClient.SportWithName(game.Sport.Name).Single();

            _graphClient.CreateRelationship(gameNode, new GameToSportRelationship(sportNode.Reference)); 
            
            var locationNode = _graphClient.LocationWithName(game.Location.Name).Single();

            _graphClient.CreateRelationship(gameNode, new GameToLocationRelationship(locationNode.Reference));

            foreach (var profileId in gameWithoutTeams.PlayersIds)
            {
                var profileNode = _graphClient.ProfileWithId(profileId).Single();

                _graphClient.CreateRelationship(profileNode.Reference, new PlaysInGameRelationship(gameNode));
            }

            return true;
        }

        public Game GetById(string id)
        {
            var gameQuery = _graphClient.GameNodeWithId(id);

            var locationForGame = gameQuery.OutE(RelationsTypes.GameToLocation).InV<Location>().Single();
            var sportForGame = gameQuery.OutE(RelationsTypes.GameToSport).InV<Sport>().Single();

            var gameNode = _graphClient.GameNodeWithId(id).Single().Data;

            var game = new GameWithoutTeams(gameNode.DateTime,
                                            new Sport(sportForGame.Data.Name),
                                            new Location(locationForGame.Data.Name))
                           {
                               Id = gameNode.Id,
                               MinPlayers = gameNode.MinPlayers,
                               MaxPlayers = gameNode.MaxPlayers,
                               IsPrivate = gameNode.IsPrivate
                           };

            game.PlayersIds = gameQuery.PlayersForGame().Select(x => x.Data.ProfileId).ToList();

            return game;
        }


        public IList<Game> FindByLocation(string location)
        {
            throw new NotImplementedException();
        }

        public void CreateGame(string sportName)
        {
            //var sport = new Sport(sportName);

            if (!GameBaseNodeExists())
                CreateGameBaseNode();

            //var sportBaseNode = _graphClient.SportBaseNode().Single();
            //var sportNode = _graphClient.Create(sport);

            //_graphClient.CreateRelationship(sportBaseNode.Reference, new GameRelationship(sportNode));
        }

        private void CreateGameBaseNode()
        {
            var gameBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.GameBase});

            var rootNode = _graphClient.RootNode;

            _graphClient.CreateRelationship(rootNode, new BaseNodeRelationship(gameBaseNode));
        }

        private bool GameBaseNodeExists()
        {
            return _graphClient.GameBaseNode().Any();
        }
    }
}