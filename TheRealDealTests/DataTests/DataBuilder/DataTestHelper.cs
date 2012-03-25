using System;
using System.Collections.Generic;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Login;
using RecreateMeSql.Mappers;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public static class DataTestHelper
    {
        public static IList<Account> GetAllAccountNodes()
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();

            var nodes = graphClient.RootNode.OutE("Account").InV<Account>();

            var listOfAccounts = new List<Account>();

            var accountMapper = new AccountMapper();

            foreach (var node in nodes)
            {
                listOfAccounts.Add(accountMapper.Map(node));
            }

            return listOfAccounts;
        }
    }
}