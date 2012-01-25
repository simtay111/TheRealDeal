using System;
using RecreateMe.Locales;

namespace RecreateMeSql
{
    public class LocationRepository : ILocationRepository
    {
        public Location FindByName(string name)
        {
            return TestData.CreateLocation1();
        }
    }
}