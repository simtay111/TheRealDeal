using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
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
            : this(GraphClientFactory.Create()) { }

        public bool SaveOrUpdate(Game game)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();

            var gameBaseNode = _graphClient.GameBaseNode().Single();

            var gameNode = _graphClient.Create(game);

            _graphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = _graphClient.SportWithName(game.Sport.Name).Single();

            _graphClient.CreateRelationship(gameNode, new GameToSportRelationship(sportNode.Reference));

            var locationNode = _graphClient.LocationWithName(game.Location.Name).Single();

            _graphClient.CreateRelationship(gameNode, new GameToLocationRelationship(locationNode.Reference));

            if (!game.HasTeams)
            {
                var gameWithoutTeams = game as GameWithoutTeams;
                foreach (var profileId in gameWithoutTeams.PlayersIds)
                {
                    var profileNode = _graphClient.ProfileWithId(profileId).Single();
                    _graphClient.CreateRelationship(profileNode.Reference, new PlaysInGameRelationship(gameNode));
                }
                return true;
            }

            var gameWithTeams = game as GameWithTeams;
            foreach (var teamId in gameWithTeams.TeamsIds)
            {
                var teamNode = _graphClient.TeamWithId(teamId).Single();
                _graphClient.CreateRelationship(teamNode.Reference, new TeamInGameRelationship(gameNode));
            }

            return true;
        }

        public Game GetById(string id)
        {
            var gameQuery = _graphClient.GameWithoutTeamsWithId(id);

            var locationForGame = gameQuery.OutE(RelationsTypes.GameToLocation).InV<Location>().Single().Data.Name;
            var sportForGame = gameQuery.OutE(RelationsTypes.GameToSport).InV<Sport>().Single().Data.Name;

            var gameData = _graphClient.GameWithoutTeamsWithId(id).Single().Data;

            if (!gameData.HasTeams)
            {
                return MapGameWithoutTeams(gameData, gameQuery, sportForGame, locationForGame);
            }

            return MapGameWithTeams(gameData, gameQuery, sportForGame, locationForGame);
        }

        private static GameWithTeams MapGameWithTeams(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery, string sportForGame,
                                                      string locationForGame)
        {
            var gameWTeams = MapGenericGameData((x, y, z) => new GameWithTeams(x, y, z), locationForGame, gameData, sportForGame);

            gameWTeams.TeamsIds = gameQuery.TeamsForGame().Select(x => x.Data.Id).ToList();
            return gameWTeams;
        }

        private static GameWithoutTeams MapGameWithoutTeams(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery,
                                                            string sportForGame, string locationForGame)
        {
            var gameWoTeams = MapGenericGameData((x, y, z) => new GameWithoutTeams(x, y, z), locationForGame, gameData,
                                                 sportForGame);

            gameWoTeams.PlayersIds = gameQuery.PlayersForGame().Select(x => x.Data.ProfileId).ToList();
            return gameWoTeams;
        }

        private static T MapGenericGameData<T>(Func<DateTimeOffset, Sport, Location, T> createGame, string locationForGame, RetrievedGame gameData, string sportForGame) where T : IGame
        {
            var gameWoTeams = createGame(gameData.DateTime,
                                         new Sport(sportForGame),
                                         new Location(locationForGame));

            gameWoTeams.Id = gameData.Id;
            gameWoTeams.MinPlayers = gameData.MinPlayers;
            gameWoTeams.MaxPlayers = gameData.MaxPlayers;
            gameWoTeams.IsPrivate = gameData.IsPrivate;


            return gameWoTeams;
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