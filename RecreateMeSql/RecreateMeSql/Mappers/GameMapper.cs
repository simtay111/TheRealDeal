using System;
using Neo4jClient.Gremlin;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMeSql.Mappers
{
    public class GameMapper
    {
        private string _gameCreator;

        public Game Map(IGremlinNodeQuery<RetrievedGame> gameQuery)
        {
            //var location = gameQuery.OutE(RelationsTypes.GameToLocation).InV<Location>().Single().Data.Name;
            //var sport = gameQuery.OutE(RelationsTypes.GameToSport).InV<Sport>().Single().Data.Name;
            //_gameCreator = gameQuery.InE(RelationsTypes.CreatedBy).OutV<Profile>().Single().Data.ProfileId;
            //var genericGameData = gameQuery.Single().Data;

            //if (!genericGameData.HasTeams)
            //{
            //    return MapGameWithoutTeams(genericGameData, gameQuery, sport, location);
            //}

            //return MapGameWithTeams(genericGameData, gameQuery, sport, location);
            throw new NotImplementedException();
        }

        private GameWithTeams MapGameWithTeams(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery, string sportForGame,
                                                      string locationForGame)
        {
            //var gameWTeams = MapGenericGameData((x, y, z) => new GameWithTeams(x, y, z), locationForGame, gameData, sportForGame);

            //gameWTeams.TeamsIds = gameQuery.TeamsForGame().Select(x => x.Data.Id).ToList();
            //return gameWTeams;
            throw new NotImplementedException();
        }

        private PickUpGame MapGameWithoutTeams(RetrievedGame gameData, IGremlinNodeQuery<RetrievedGame> gameQuery,
                                                            string sportForGame, string locationForGame)
        {
            //var gameWoTeams = MapGenericGameData((x, y, z) => new PickUpGame(x, y, z), locationForGame, gameData,
            //                                     sportForGame);

            //gameWoTeams.PlayersIds = gameQuery.PlayersForGame().Select(x => x.Data.ProfileId).ToList();
            //return gameWoTeams;
            throw new NotImplementedException();
        }

        //private T MapGenericGameData<T>(Func<DateTimeOffset, Sport, Location, T> createGame, string locationForGame, RetrievedGame gameData, string sportForGame) where T : IGame
        //{
        //    var game = createGame(gameData.DateTime,
        //                                 new Sport(sportForGame),
        //                                 new Location(locationForGame));

        //    game.Id = gameData.Id;
        //    game.MinPlayers = gameData.MinPlayers;
        //    game.MaxPlayers = gameData.MaxPlayers;
        //    game.IsPrivate = gameData.IsPrivate;
        //    game.Creator = _gameCreator;

        //    return game;
        //}
    }
}