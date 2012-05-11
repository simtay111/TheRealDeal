﻿using RecreateMe.Locales;
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
            _factory = new OrmLiteConnectionFactory(@"Server=e2babdc8-29c6-4349-a215-a04b000468e0.sqlserver.sequelizer.com;Database=dbe2babdc829c64349a215a04b000468e0;User ID=lyyfhvctjjrscbps;Password=XhHC4xWZbpWheqSSQCJbJfTB6YPctfUoHgZxncwcihmcc8QeK5bKAJhRvfFeRUF4;", SqlServerOrmLiteDialectProvider.Instance);
        }

        public static OrmLiteConnectionFactory Create()
        {
            return _factory;
        }
    }
}