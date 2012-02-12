namespace RecreateMe.Locales
{
    public interface ILocationRepository
    {
        Location FindByName(string name);
        bool CreateLocation(string name);
    }
}