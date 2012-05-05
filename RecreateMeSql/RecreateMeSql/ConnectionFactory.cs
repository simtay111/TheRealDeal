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
        }

        public static OrmLiteConnectionFactory Create()
        {
            return _factory;
        }
    }
}