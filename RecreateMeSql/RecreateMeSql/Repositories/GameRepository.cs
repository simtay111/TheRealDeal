using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GraphClient _graphClient;
        private readonly GameMapper _gameMapper;

        public GameRepository(GraphClient graphClient, GameMapper gameMapper)
        {
            _graphClient = graphClient;
            _gameMapper = gameMapper;
        }

        public GameRepository()
            : this(GraphClientFactory.Create(), new GameMapper()) { }

        public bool Save(Game game)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();

            var gameNode = CreateGameNodeAndSaveGenericData(game);

            if (!game.HasTeams)
            {
                return SaveGameWithoutTeamData(game, gameNode);
            }

            return SaveGameWithTeamData(game, gameNode);
        }

        private bool SaveGameWithTeamData(Game game, NodeReference gameNode)
        {
            var gameWithTeams = game as GameWithTeams;
            foreach (var teamId in gameWithTeams.TeamsIds)
            {
                var teamNode = _graphClient.TeamWithId(teamId).Single();
                _graphClient.CreateRelationship(teamNode.Reference, new TeamInGameRelationship(gameNode));
            }
            return true;
        }

        private bool SaveGameWithoutTeamData(Game game, NodeReference gameNode)
        {
            var gameWithoutTeams = game as GameWithoutTeams;
            foreach (var profileId in gameWithoutTeams.PlayersIds)
            {
                var profileNode = _graphClient.ProfileWithId(profileId).Single();
                _graphClient.CreateRelationship(profileNode.Reference, new PlaysInGameRelationship(gameNode));
            }
            return true;
        }

        private NodeReference<Game> CreateGameNodeAndSaveGenericData(Game game)
        {
            var gameBaseNode = _graphClient.GameBaseNode().Single();

            var gameNode = _graphClient.Create(game);

            _graphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = _graphClient.SportWithName(game.Sport.Name).Single();

            _graphClient.CreateRelationship(gameNode, new GameToSportRelationship(sportNode.Reference));

            var locationNode = _graphClient.LocationWithName(game.Location.Name).Single();

            _graphClient.CreateRelationship(gameNode, new GameToLocationRelationship(locationNode.Reference));
            return gameNode;
        }

        public Game GetById(string id)
        {
            var gameQuery = _graphClient.GameWithId(id);
           
            return _gameMapper.Map(gameQuery);
        }

        public IList<Game> FindByLocation(string location)
        {
            throw new NotImplementedException();
        }

        public void CreateGame(string sportName)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();
        }

        private void CreateGameBaseNode()
        {
            var gameBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.GameBase });

            var rootNode = _graphClient.RootNode;

            _graphClient.CreateRelationship(rootNode, new BaseNodeRelationship(gameBaseNode));
        }

        private bool GameBaseNodeExists()
        {
            return _graphClient.GameBaseNode().Any();
        }
    }
}