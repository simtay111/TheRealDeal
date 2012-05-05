using System.Collections.Generic;
using System.Data;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.GameRelationships;
using RecreateMeSql.Relationships.ProfileRelationships;
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
            }
        }

        public PickUpGame GetPickUpGameById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<PickUpGame> FindPickUpGameByLocation(string location)
        {
            throw new System.NotImplementedException();
        }

        public IList<PickUpGame> GetPickupGamesForProfile(string profileId)
        {
            throw new System.NotImplementedException();
        }

        public void AddPlayerToGame(string gameId, string profileId)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteGame(string gameId)
        {
            throw new System.NotImplementedException();
        }

        public void RemovePlayerFromGame(string profileId, string gameId)
        {
            throw new System.NotImplementedException();
        }

        public List<PickUpGame> GetByCreated(string profileId)
        {
            throw new System.NotImplementedException();
        }
    }
}