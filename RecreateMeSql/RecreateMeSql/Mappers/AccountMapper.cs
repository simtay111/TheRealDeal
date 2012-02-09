using RecreateMe.Login;

namespace RecreateMeSql.Mappers
{
    public class AccountMapper
    {
        public Account Map(Neo4jClient.Node<Account> accountNode  )
        {
            return new Account()
                       {
                           Password = accountNode.Data.Password,
                           UserName = accountNode.Data.UserName
                       };
        }
    }
}