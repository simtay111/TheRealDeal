using RecreateMe.Locales;
using TheRealDealTests.DomainTests;

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