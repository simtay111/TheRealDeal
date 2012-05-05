
using ServiceStack.DataAnnotations;

namespace RecreateMe.Locales
{
    public class Location
    {
        [PrimaryKey]
        public string Name { get; set; }

        public Location(){}

        public Location( string name)
        {
            Name = name;
        }

        public static Location Default
        {
            get
            {
                return new Location(Constants.DefaultLocationName);
            }
        }
    }
}