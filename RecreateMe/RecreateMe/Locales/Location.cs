
namespace RecreateMe.Locales
{
    public class Location
    {
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