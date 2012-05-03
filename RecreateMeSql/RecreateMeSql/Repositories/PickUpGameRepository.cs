using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.GameRelationships;
using RecreateMeSql.Relationships.ProfileRelationships;

namespace RecreateMeSql.Repositories
{
    public class PickUpGameRepository : BaseRepository, IPickUpGameRepository
    {
        private readonly GameMapper _gameMapper;

        public PickUpGameRepository(GraphClient graphClient, GameMapper gameMapper) :base(graphClient)
        {
            _gameMapper = gameMapper;
        }

        public PickUpGameRepository()
            : this(GraphClientFactory.Create(), new GameMapper()) { }

        public void SavePickUpGame(PickUpGame game)
        {
            CreateGameBaseNode();

            var gameNode = CreatePickUpGameNodeAndSaveGenericData(game);

            SavePickUpGameData(game, gameNode);
        }

        public PickUpGame GetPickUpGameById(string id)
        {
            var pickUpGameQuery = GraphClient.GameWithId(id);

            return _gameMapper.MapPickupGame(pickUpGameQuery);
        }

        public IList<PickUpGame> FindPickUpGameByLocation(string location)
        {
            var games = GraphClient.PickUpGamesAtLocation(location).Select(x => x.Data.Id).ToList();
            return games.Select(x => _gameMapper.MapPickupGame(GraphClient.GameWithId(x))).ToList();
        }

        public IList<PickUpGame> GetPickupGamesForProfile(string profileId)
        {
            var games = GraphClient.ProfileWithId(profileId).PickUpGamesForProfile().Select(x => x.Data.Id).ToList();

            return games.Select(game => _gameMapper.MapPickupGame(GraphClient.GameWithId(game))).ToList();
        }

        public void AddPlayerToGame(string gameId, string profileId)
        {
            var gameNode = GraphClient.GameWithId(gameId).Single();
            CreatePlaysInGameRelationship(gameNode.Reference, profileId);
        }

        public List<PickUpGame> GetByCreated(string profileId)
        {
            var games = GraphClient.ProfileWithId(profileId).GamesThisProfileCreated().Select(x => x.Data.Id).ToList();

            return games.Select(x => _gameMapper.MapPickupGame(GraphClient.GameWithId(x))).ToList();
        }

        public void DeleteGame(string gameId)
        {
            var game = GraphClient.GameWithId(gameId).SingleOrDefault();

            if (game == null)
                return;

            GraphClient.Delete(game.Reference, DeleteMode.NodeAndRelationships);
        }

        public void RemovePlayerFromGame(string profileId, string gameId)
        {
            var gameNode = GraphClient.GameWithId(gameId).Single();
            var relationship = GraphClient.ProfileWithId(profileId).OutE(RelationsTypes.PlaysInGame).Where(x => x.EndNodeReference == gameNode.Reference).Single();
            GraphClient.DeleteRelationship(relationship.Reference);
        }

        private void CreatePlaysInGameRelationship(NodeReference gameNode, string profileId)
        {
            var profileNode = GraphClient.ProfileWithId(profileId).Single();
            GraphClient.CreateRelationship(profileNode.Reference, new PlaysInGameRelationship(gameNode));
        }

        private void SavePickUpGameData(PickUpGame game, NodeReference gameNode)
        {
            foreach (var profileId in game.PlayersIds)
            {
                CreatePlaysInGameRelationship(gameNode, profileId);
            }
        }

        private NodeReference<PickUpGame> CreatePickUpGameNodeAndSaveGenericData(PickUpGame game)
        {
            var gameBaseNode = GraphClient.GameBaseNode().Single();

            var gameNode = GraphClient.Create(game);

            GraphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = GraphClient.SportWithName(game.Sport.Name).Single();

            GraphClient.CreateRelationship(sportNode.Reference, new GameToSportRelationship(gameNode));

            var locationNode = GraphClient.LocationWithName(game.Location.Name).Single();

            GraphClient.CreateRelationship(locationNode.Reference, new GameToLocationRelationship(gameNode));

            var profileNode = GraphClient.ProfileWithId(game.Creator).Single();

            GraphClient.CreateRelationship(profileNode.Reference, new CreatedByRelationship(gameNode));
            return gameNode;
        }
    }
}