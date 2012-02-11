namespace RecreateMe.Locales
{
    public interface ILocationRepository
    {
        Location FindByName(string name);
        void CreateLocation(string name);
    }
}