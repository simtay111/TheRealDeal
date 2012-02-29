using System;
using System.Linq;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;
using RecreateMeSql.Relationships;

namespace RecreateMeSql.Mappers
{
    public class GameMapper
    {
        public Game Map(IGremlinNodeQuery<RetrievedGame> gameQuery)
        {
            var location = gameQuery.OutE(RelationsTypes.GameToLocation).InV<Location>().Single().Data.Name;
            var sport = gameQuery.OutE(RelationsTypes.GameToSport).InV<Sport>().Single().Data.Name;
            var genericGameData = gameQuery.Single().Data;

            if (!genericGameData.HasTeams)
            {
                return MapGameWithoutTeams(genericGameData, gameQuery, sport, location);
            }

            return MapGameWithTeams(genericGameData, gameQuery, sport, location);
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
    }
}