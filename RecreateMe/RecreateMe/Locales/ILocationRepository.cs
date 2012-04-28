using System.Collections.Generic;

namespace RecreateMe.Locales
{
    public interface ILocationRepository
    {
        Location FindByName(string name);
        bool CreateLocation(string name);
        List<string> GetNamesOfAllLocations();
    }
}