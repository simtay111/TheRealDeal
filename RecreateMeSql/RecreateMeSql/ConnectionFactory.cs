using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Configuration;

namespace RecreateMeSql
{
    public static class ConnectionFactory
    {
        private static OrmLiteConnectionFactory _factory;

        static ConnectionFactory()
        {
            //_factory = new OrmLiteConnectionFactory(@"Server=(local);Database=PortlandPickUp;Trusted_Connection=True;", SqlServerOrmLiteDialectProvider.Instance);
            _factory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["PortlandPickUp"].ConnectionString, SqlServerOrmLiteDialectProvider.Instance);
            
        }

        public static OrmLiteConnectionFactory Create()
        {
            return _factory;
        }
    }
}