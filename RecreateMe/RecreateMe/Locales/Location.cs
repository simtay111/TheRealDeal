using System.Collections.Generic;

namespace RecreateMe.Locales
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Location(int id)
        {
            Id = id;
        }

        public Location(int id, string name) : this(id)
        {
            Name = name;
        }

        public static Location Default
        {
            get
            {
                return new Location(Constants.DefaultLocationId, Constants.DefaultLocationName);
            }
        }
    }
}