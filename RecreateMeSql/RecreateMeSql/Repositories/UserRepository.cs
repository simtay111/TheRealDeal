using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Login;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.AccountRelationships;

namespace RecreateMeSql.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(GraphClient graphClient) : base (graphClient)
        {
        }

        public UserRepository(): this(GraphClientFactory.Create())
        {
        }

        public void CreateUser(string userName, string password)
        {
            var account = new Account { Password = password, AccountName = userName };

            var rootnode = GraphClient.Get(new RootNode());

            var accountNode = GraphClient.Create(account);

            GraphClient.CreateRelationship(rootnode.Reference, new AccountRelationship(accountNode));
        }

        public bool AlreadyExists(string username)
        {
            var nodes = GraphClient.RootNode.OutE(RelationsTypes.Account).InV<Account>(n => n.AccountName == username);

            return nodes.Any();
        }

        public bool FoundUserByNameAndPassword(string username, string password)
        {
            var accountNode = GraphClient.RootNode.OutE(RelationsTypes.Account).InV<Account>(n => (n.AccountName == username)).FirstOrDefault();

            if (accountNode == null)
                return false;

            return accountNode.Data.Password == password;
        }
    }
}