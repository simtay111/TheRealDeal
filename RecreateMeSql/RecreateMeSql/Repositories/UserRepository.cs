using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Login;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;

namespace RecreateMeSql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GraphClient _graphClient;

        public UserRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public UserRepository(): this(GraphClientFactory.Create())
        {
        }

        public void CreateUser(string userName, string password)
        {
            var account = new Account() { Password = password, AccountName = userName };

            var rootnode = _graphClient.Get(new RootNode());

            var accountNode = _graphClient.Create(account);

            _graphClient.CreateRelationship(rootnode.Reference, new AccountRelationship(accountNode));
        }

        public bool AlreadyExists(string username)
        {
            var nodes = _graphClient.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.AccountName == username);

            return nodes.Any();
        }

        public bool FoundUserByNameAndPassword(string username, string password)
        {
            var accountNode = _graphClient.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => (n.AccountName == username)).FirstOrDefault();

            if (accountNode == null)
                return false;

            return accountNode.Data.Password == password;
        }
    }
}