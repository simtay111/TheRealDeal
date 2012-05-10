using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using RecreateMe.Profiles;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMeSql.LinkingClasses;
using ServiceStack.OrmLite;

namespace RecreateMeSql.Repositories
{
    public class PickUpGameRepository : IPickUpGameRepository
    {
        private readonly OrmLiteConnectionFactory _connectionFactory;

        public PickUpGameRepository(OrmLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public PickUpGameRepository()
            : this(ConnectionFactory.Create())
        {
        }

        public void SavePickUpGame(PickUpGame game)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                dbCmd.Save(game);
                foreach (var playerId in game.PlayersIds)
                {
                    AddPlayerToGame(game.Id, playerId);
                }
            }
        }

        public PickUpGame GetPickUpGameById(string id)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                return GetGameById(id, dbCmd);
            }
        }

        private PickUpGame GetGameById(string id, IDbCommand dbCmd)
        {
            var game = dbCmd.GetById<PickUpGame>(id);
            MapPlayersInGame(dbCmd, game);
            return game;
        }

        public IList<PickUpGame> FindPickUpGameByLocation(string location)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var games = dbCmd.Select<PickUpGame>("Location = {0}", location);
                Trace.WriteLine("PickUpGameRepository-FindPickUpGameByLocation: Start Mapping Games");
                games.ForEach(x => MapPlayersInGame(dbCmd, x));
                Trace.WriteLine("PickUpGameRepository-FindPickUpGameByLocation: End Mapping Games");
                return games;
            }
        }

        public IList<PickUpGame> GetPickupGamesForProfile(string profileId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                var gameIds = dbCmd.Select<PlayerInGameLink>("PlayerId = {0}", profileId).Select(x => x.GameId);
                var games =  gameIds.Select(x => GetGameById(x, dbCmd)).ToList();
                games.ForEach(x => MapPlayersInGame(dbCmd, x));
                return games;
            }
        }

        public void AddPlayerToGame(string gameId, string profileId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                dbCmd.Insert(new PlayerInGameLink { GameId = gameId, PlayerId = profileId });
            }
        }

        public void DeleteGame(string gameId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                try
                {
                    var game = GetGameById(gameId, dbCmd);
                    foreach (var player in game.PlayersIds)
                        RemovePlayerFromGame(player, gameId);
                    dbCmd.DeleteById<PickUpGame>(gameId);
                }
                catch (ArgumentNullException ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }

        public void RemovePlayerFromGame(string profileId, string gameId)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerInGame =
                    dbCmd.Select<PlayerInGameLink>("GameId = {0} and PlayerId = {1}", gameId, profileId).SingleOrDefault
                        ();
                if (playerInGame == null)
                    return;
                dbCmd.DeleteById<PlayerInGameLink>(playerInGame.Id);
            }
        }

        public List<PickUpGame> GetByCreated(string profileId)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var games = dbCmd.Select<PickUpGame>("Creator = {0}", profileId);
                games.ForEach(x => MapPlayersInGame(dbCmd, x));
                return games;
            }
        }

        private void MapPlayersInGame(IDbCommand dbCmd, PickUpGame game)
        {
            game.PlayersIds =
                dbCmd.Select<PlayerInGameLink>(x => x.GameId == game.Id).Select(x => x.PlayerId).ToList();
        }
    }
}