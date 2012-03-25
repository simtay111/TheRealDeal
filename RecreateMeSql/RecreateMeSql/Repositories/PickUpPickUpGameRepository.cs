using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.GameRelationships;
using RecreateMeSql.Relationships.ProfileRelationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class PickUpPickUpGameRepository : IPickUpGameRepository
    {
        private readonly GraphClient _graphClient;
        private readonly GameMapper _gameMapper;

        public PickUpPickUpGameRepository(GraphClient graphClient, GameMapper gameMapper)
        {
            _graphClient = graphClient;
            _gameMapper = gameMapper;
        }

        public PickUpPickUpGameRepository()
            : this(GraphClientFactory.Create(), new GameMapper()) { }

        public bool Save(Game game)
        {
            //if (!GameBaseNodeExists())
            //    CreateGameBaseNode();

            //var gameNode = CreateGameNodeAndSaveGenericData(game);

            ////if (!game.HasTeams)
            ////{
            ////    return SavePickUpGameData(game, gameNode);
            ////}

            //return SaveGameWithTeamData(game, gameNode);
            throw new NotImplementedException();
        }

        public void SavePickUpGame(PickUpGame game)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();

            var gameNode = CreatePickUpGameNodeAndSaveGenericData(game);

            SavePickUpGameData(game, gameNode);
        }

        public PickUpGame GetPickUpGameById(string id)
        {
            var pickUpGameQuery = _graphClient.GameWithId(id);

            return _gameMapper.MapPickupGame(pickUpGameQuery);
        }

        public IList<PickUpGame> FindPickUpGameByLocation(string location)
        {
            var games = _graphClient.PickUpGamesAtLocation(location).Select(x => x.Data.Id).ToList();
            return games.Select(x => _gameMapper.MapPickupGame(_graphClient.GameWithId(x))).ToList();
        }

        public IList<PickUpGame> GetPickupGamesForProfile(string profileId)
        {
            var gamesWithTeams = _graphClient.ProfileWithId(profileId).GamesWithoutTeamsForProfile().Select(x => x.Data.Id).ToList();

            return gamesWithTeams.Select(game => _gameMapper.MapPickupGame(_graphClient.GameWithId(game))).ToList();
        }

        public void AddPlayerToGame(string gameId, string profileId)
        {
            var gameNode = _graphClient.GameWithId(gameId).Single();
            CreatePlaysInGameRelationship(gameNode.Reference, profileId);
        }

        public void CreateGameBaseNode(string sportName)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();
        }

        private void CreatePlaysInGameRelationship(NodeReference gameNode, string profileId)
        {
            var profileNode = _graphClient.ProfileWithId(profileId).Single();
            _graphClient.CreateRelationship(profileNode.Reference, new PlaysInGameRelationship(gameNode));
        }

        private void SavePickUpGameData(PickUpGame game, NodeReference gameNode)
        {
            foreach (var profileId in game.PlayersIds)
            {
                CreatePlaysInGameRelationship(gameNode, profileId);
            }
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

        private NodeReference<PickUpGame> CreatePickUpGameNodeAndSaveGenericData(PickUpGame game)
        {
            var gameBaseNode = _graphClient.GameBaseNode().Single();

            var gameNode = _graphClient.Create(game);

            _graphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = _graphClient.SportWithName(game.Sport.Name).Single();

            _graphClient.CreateRelationship(sportNode.Reference, new GameToSportRelationship(gameNode));

            var locationNode = _graphClient.LocationWithName(game.Location.Name).Single();

            _graphClient.CreateRelationship(locationNode.Reference, new GameToLocationRelationship(gameNode));

            var profileNode = _graphClient.ProfileWithId(game.Creator).Single();

            _graphClient.CreateRelationship(profileNode.Reference, new CreatedByRelationship(gameNode));
            return gameNode;
        }
    }
}