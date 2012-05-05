using System.Collections.Generic;
using System.Data;
using System.Linq;
using RecreateMe.Locales;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace RecreateMeSql.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly OrmLiteConnectionFactory _connectionFactory;

        public LocationRepository(OrmLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public LocationRepository()
            : this(ConnectionFactory.Create())
        {
        }

        public Location FindByName(string name)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                return dbCmd.GetById<Location>(name);
            }
        }

        public bool CreateLocation(string name)
        {
             using (IDbConnection db = _connectionFactory.OpenDbConnection())
             using (IDbCommand dbCmd = db.CreateCommand())
             {
                 var location = dbCmd.GetByIdOrDefault<Location>(name);
                 if (location != null)
                     return false;
                 dbCmd.Insert(new Location(name));
             }
            return true;
        }

        public List<string> GetNamesOfAllLocations()
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                return dbCmd.Each<Location>().Select(x => x.Name).ToList();
            }
        }
    }
}