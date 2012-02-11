using RecreateMe.Locales;

namespace RecreateMeSql.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public Location FindByName(string name)
        {
            return TestData.CreateLocation1();
        }
    }
}