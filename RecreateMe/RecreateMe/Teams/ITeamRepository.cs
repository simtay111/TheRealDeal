using System.Collections.Generic;

namespace RecreateMe.Teams
{
    public interface ITeamRepository
    {
        bool Save(Team team);
        Team GetById(string id);
        IList<Team> GetTeamsForProfile(string id);
        void DeleteTeam(string teamId);
    }
}