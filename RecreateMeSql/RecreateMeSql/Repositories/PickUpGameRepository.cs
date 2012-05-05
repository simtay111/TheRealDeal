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
            game.PlayersIds = dbCmd.Each<PlayerInGame>("GameId = {0}", id).Select(y => y.PlayerId).ToList();
            return game;
        }

        public IList<PickUpGame> FindPickUpGameByLocation(string location)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                return dbCmd.Select<PickUpGame>("Location = {0}", location);
            }
        }

        public IList<PickUpGame> GetPickupGamesForProfile(string profileId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                var gameIds = dbCmd.Select<PlayerInGame>("PlayerId = {0}", profileId).Select(x => x.GameId);
                return gameIds.Select(x => GetGameById(x, dbCmd)).ToList();
            }
        }

        public void AddPlayerToGame(string gameId, string profileId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                dbCmd.Insert(new PlayerInGame { GameId = gameId, PlayerId = profileId });
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
                    dbCmd.Select<PlayerInGame>("GameId = {0} and PlayerId = {1}", gameId, profileId).SingleOrDefault
                        ();
                if (playerInGame == null)
                    return;
                dbCmd.DeleteById<PlayerInGame>(playerInGame.Id);
            }
        }

        public List<PickUpGame> GetByCreated(string profileId)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                return dbCmd.Select<PickUpGame>("Creator = {0}", profileId);
            }
        }
    }
}