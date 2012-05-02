using System;
using System.Linq;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMeSql.Relationships;

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

        public TeamGame MapTeamGame(IGremlinNodeQuery<RetrievedGame> gameQuery)
        {
            var location = gameQuery.InE(RelationsTypes.GameToLocation).OutV<Location>().Single().Data.Name;
            var sport = gameQuery.InE(RelationsTypes.GameToSport).OutV<Sport>().Single().Data.Name;
            _gameCreator = gameQuery.InE(RelationsTypes.CreatedBy).OutV<Profile>().Single().Data.ProfileId;
            var genericGameData = gameQuery.Single().Data;

            return MapGameWithTeams(genericGameData, gameQuery, sport, location);
        }

        private TeamGame MapGameWithTeams(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery, string sportForGame,
                                                      string locationForGame)
        {
            var gameWTeams = MapGenericGameData((x, y, z) => new TeamGame(x, y, z), locationForGame, gameData, sportForGame);

            gameWTeams.TeamsIds = gameQuery.TeamsForGame().Select(x => x.Data.Id).ToList();
            return gameWTeams;
        }

        private PickUpGame MapPickupGame(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery,
                                                            string sportForGame, string locationForGame)
        {
            var pickupGame = MapGenericGameData((x, y, z) => new PickUpGame(x, y, z), locationForGame, gameData,
                                                 sportForGame);

            pickupGame.PlayersIds = gameQuery.PlayersForGame().Select(x => x.Data.ProfileId).ToList();
            pickupGame.ExactLocation = gameData.ExactLocation;
            return pickupGame;
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