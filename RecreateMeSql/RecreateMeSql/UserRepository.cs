using RecreateMe.Login;

namespace RecreateMeSql
{

    public class UserRepository : IUserRepository
    {
        public void CreateUser(string userName, string password)
        {
        }

        public bool AlreadyExists(string username)
        {
            return false;
        }

        public bool FoundUserByNameAndPassword(string username, string password)
        {
            return true;
        }
    }
}