using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Login;
using RecreateMeSql.Relationships;

namespace RecreateMeSql
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser(string userName, string password)
        {
            var account = new Account() {Password = password, UserName = userName};

            var graphClient = CreateGraphClient();

            var rootnode = graphClient.Get(new RootNode());

            var accountNode = graphClient.Create(account);

            graphClient.CreateRelationship(rootnode.Reference, new AccountRelationship(accountNode));
        }

        public bool AlreadyExists(string username)
        {
            var gc = CreateGraphClient();

            var nodes = gc.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.UserName == username);

            return nodes.Any();
        }

        public bool FoundUserByNameAndPassword(string username, string password)
        {
            var gc = CreateGraphClient();

            var accountNode = gc.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => (n.UserName == username)).FirstOrDefault();

            if (accountNode == null)
                return false;

            return accountNode.Data.Password == password;
        }

        private GraphClient CreateGraphClient()
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();
            return graphClient;
        }
    }
}