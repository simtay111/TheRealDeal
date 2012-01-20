namespace RecreateMe.Sports
{
    public interface ISportRepository
    {
        Sport FindByName(string name);
    }
}