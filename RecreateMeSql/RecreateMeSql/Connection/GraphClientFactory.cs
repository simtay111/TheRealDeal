using System;
using Neo4jClient;

namespace RecreateMeSql.Connection
{
    public class GraphClientFactory
    {
        private static GraphClient GraphClient;

        public static GraphClient Create()
        {
            if (GraphClient == null)
            {
                GraphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));
                GraphClient.Connect();
            }
                
            return GraphClient;
        }
    }
}