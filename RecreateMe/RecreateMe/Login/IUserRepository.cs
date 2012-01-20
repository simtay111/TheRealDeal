namespace RecreateMe.Login
{
    public interface IUserRepository
    {
        void CreateUser(string userName, string password);
        bool AlreadyExists(string username);
        bool FoundUserByNameAndPassword(string username, string password);
    }
}