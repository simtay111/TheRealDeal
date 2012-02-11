using System;
using Neo4jClient;

namespace RecreateMeSql.Connection
{
    public class GraphClientFactory
    {
        private static GraphClient _graphClient;

        public static GraphClient Create()
        {
            if (_graphClient == null)
            {
                _graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));
                _graphClient.Connect();
            }
                
            return _graphClient;
        }
    }
}