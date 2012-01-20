using System;
using System.Data;
using System.Web.Helpers;
using RecreateMe.Login;

namespace RecreateMeSql
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool AlreadyExists(string username)
        {
            throw new NotImplementedException();
        }

        public bool FoundUserByNameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
