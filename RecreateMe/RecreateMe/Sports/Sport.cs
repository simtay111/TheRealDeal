using ServiceStack.DataAnnotations;

namespace RecreateMe.Sports
{
    public class Sport
    {
        [PrimaryKey]
        public string Name { get; set; }

        public Sport(string name)
        {
            Name = name;
        }

        public Sport()
        {
        }
    }
}