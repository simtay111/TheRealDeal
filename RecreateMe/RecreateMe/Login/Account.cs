using ServiceStack.DataAnnotations;

namespace RecreateMe.Login
{
    public class Account
    {
        [PrimaryKey]
        public string AccountName { get; set; } 
        public string Password { get; set; } 
    }
}