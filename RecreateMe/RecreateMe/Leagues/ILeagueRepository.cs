namespace RecreateMe.Leagues
{
    public interface ILeagueRepository
    {
        void Save(League league);
        League GetById(string id);
        void AddDivisionToLeague(League league, string divisionId);
    }
}