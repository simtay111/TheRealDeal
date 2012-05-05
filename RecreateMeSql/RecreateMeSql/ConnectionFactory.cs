using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace RecreateMeSql
{
    public static class ConnectionFactory
    {
        private static OrmLiteConnectionFactory _factory;

        static ConnectionFactory()
        {
            _factory = new OrmLiteConnectionFactory(@"Server=(local);Database=PortlandPickUp;Trusted_Connection=True;", SqlServerOrmLiteDialectProvider.Instance);
             using (var db = _factory.OpenDbConnection())
             using (var dbCmd = db.CreateCommand())
             {
                 dbCmd.CreateTable<Account>(true);
                 dbCmd.CreateTable<Profile>(true);
                 dbCmd.CreateTable<Sport>(true);
                 dbCmd.CreateTable<Location>(true);
                 dbCmd.CreateTable<PickUpGame>(true);
             }
        }

        public static OrmLiteConnectionFactory Create()
        {
            return _factory;
        }
    }
}