using System;
using Neo4jClient.Gremlin;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMeSql.Mappers
{
    public class GameMapper
    {
        private string _gameCreator;

        public PickUpGame MapPickupGame(IGremlinNodeQuery<RetrievedGame> gameQuery)
        {
            var location = gameQuery.InE(RelationsTypes.GameToLocation).OutV<Location>().Single().Data.Name;
            var sport = gameQuery.InE(RelationsTypes.GameToSport).OutV<Sport>().Single().Data.Name;
            _gameCreator = gameQuery.InE(RelationsTypes.CreatedBy).OutV<Profile>().Single().Data.ProfileId;
            var genericGameData = gameQuery.Single().Data;

            return MapPickupGame(genericGameData, gameQuery, sport, location);
        }

        public GameWithTeams MapTeamGame(IGremlinNodeQuery<RetrievedGame> gameQuery)
        {
            var location = gameQuery.InE(RelationsTypes.GameToLocation).OutV<Location>().Single().Data.Name;
            var sport = gameQuery.InE(RelationsTypes.GameToSport).OutV<Sport>().Single().Data.Name;
            _gameCreator = gameQuery.InE(RelationsTypes.CreatedBy).OutV<Profile>().Single().Data.ProfileId;
            var genericGameData = gameQuery.Single().Data;

            return MapGameWithTeams(genericGameData, gameQuery, sport, location);
        }

        private GameWithTeams MapGameWithTeams(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery, string sportForGame,
                                                      string locationForGame)
        {
            var gameWTeams = MapGenericGameData((x, y, z) => new GameWithTeams(x, y, z), locationForGame, gameData, sportForGame);

            gameWTeams.TeamsIds = gameQuery.TeamsForGame().Select(x => x.Data.Id).ToList();
            return gameWTeams;
        }

        private PickUpGame MapPickupGame(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery,
                                                            string sportForGame, string locationForGame)
        {
            var gameWoTeams = MapGenericGameData((x, y, z) => new PickUpGame(x, y, z), locationForGame, gameData,
                                                 sportForGame);

            gameWoTeams.PlayersIds = gameQuery.PlayersForGame().Select(x => x.Data.ProfileId).ToList();
            return gameWoTeams;
        }

        private T MapGenericGameData<T>(Func<DateTimeOffset, Sport, Location, T> createGame, string locationForGame, RetrievedGame gameData, string sportForGame) where T : IAmAGame
        {
            var game = createGame(gameData.DateTime,
                                         new Sport(sportForGame),
                                         new Location(locationForGame));

            game.Id = gameData.Id;
            game.MinPlayers = gameData.MinPlayers;
            game.MaxPlayers = gameData.MaxPlayers;
            game.IsPrivate = gameData.IsPrivate;
            game.Creator = _gameCreator;

            return game;
        }
    }
}