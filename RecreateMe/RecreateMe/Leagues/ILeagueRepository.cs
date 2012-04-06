namespace RecreateMe.Leagues
{
    public interface ILeagueRepository
    {
        void Save(League league);
        League GetById(string id);
    }
}