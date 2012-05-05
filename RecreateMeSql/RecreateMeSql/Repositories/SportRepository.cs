using System.Collections.Generic;
using System.Data;
using System.Linq;
using RecreateMe.Locales;
using RecreateMe.Sports;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace RecreateMeSql.Repositories
{
    public class SportRepository :  ISportRepository
    {
        private readonly OrmLiteConnectionFactory _connectionFactory;

        public SportRepository(OrmLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public SportRepository()
            : this(ConnectionFactory.Create()) { }

        public Sport FindByName(string name)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                return dbCmd.GetById<Sport>(name);
            }
        }

        public IList<string> GetNamesOfAllSports()
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                return dbCmd.Each<Sport>().Select(x => x.Name).ToList();
            }
        }

        public void CreateSport(string sportName)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                var sport = dbCmd.GetByIdOrDefault<Location>(sportName);
                if (sport != null)
                    return;
                dbCmd.Insert(new Sport(sportName));
            }
        }
    }
}