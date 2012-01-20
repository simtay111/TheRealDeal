namespace RecreateMe.Teams
{
    public interface ITeamRepository
    {
        bool SaveOrUpdate(Team team);
        Team GetById(string id);
    }
}