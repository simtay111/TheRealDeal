using System.Data;
using RecreateMe.Login;
using ServiceStack.OrmLite;

namespace RecreateMeSql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrmLiteConnectionFactory _connectionFactory;

        public UserRepository(OrmLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public UserRepository() : this(ConnectionFactory.Create())
        {
        }

        public void CreateUser(string userName, string password)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())   
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                dbCmd.CreateTable<Account>();

                dbCmd.Insert(new Account {AccountName = userName, Password = password});
            }

            //var account = new Account { Password = password, AccountName = userName };

            //var rootnode = GraphClient.Get(new RootNode());

            //var accountNode = GraphClient.Create(account);

            //GraphClient.CreateRelationship(rootnode.Reference, new AccountRelationship(accountNode));
        }

        public bool AlreadyExists(string username)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                return dbCmd.GetByIdOrDefault<Account>(username) != null;
            }
        }

        public bool FoundUserByNameAndPassword(string username, string password)
        {
             using (IDbConnection db = _connectionFactory.OpenDbConnection())
             using (IDbCommand dbCmd = db.CreateCommand())
             {
                var user =dbCmd.GetByIdOrDefault<Account>(username);
                if (user == null)
                    return false;
                 return (user.AccountName == username && user.Password == password);
             }

        }
    }
}