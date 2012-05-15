using System.Diagnostics;
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
            try
            {
                _factory =
                    new OrmLiteConnectionFactory(
                        ConfigurationManager.ConnectionStrings["PortlandPickUp"].ConnectionString,
                        SqlServerOrmLiteDialectProvider.Instance);
            }
            catch
            {
                Trace.WriteLine("You must be testing right now... I hope...");
            }
        }

        public static OrmLiteConnectionFactory Create()
        {
            return _factory;
        }

        public static void SetConnectionString(string connString)
        {
            _factory = new OrmLiteConnectionFactory(connString, SqlServerOrmLiteDialectProvider.Instance);
        }
    }
}