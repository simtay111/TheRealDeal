using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMeSql;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class SampleDataBuilder
    {
        public void DeleteAllData()
        {
            var graphClient = BuildGraphClient();

            var nodes = graphClient.ExecuteGetAllNodesGremlin<GenericNodeType>
                ("g.V.filter{it.Id != 0}", new Dictionary<string, object>()).ToList();

            nodes.RemoveAt(0);

            foreach (var node in nodes)
            {
                graphClient.Delete(node.Reference, DeleteMode.NodeAndRelationships);
            }
        }

        private GraphClient BuildGraphClient()
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();
            return graphClient;
        }

        public void CreateData()
        {
            var userRepo = new UserRepository();

            userRepo.CreateUser("Pickles@Moo.com", "password");
            userRepo.CreateUser("Cows@Moo.com", "password");
            userRepo.CreateUser("Simtay111@Gmail.com", "password");
        }
    }

    internal class GenericNodeType
    {
    }
}