using System.Collections.Generic;
using RecreateMe.Teams;
using TheRealDealTests.DomainTests;

namespace RecreateMeSql
{
    public class TeamRepository : ITeamRepository
    {
        public bool SaveOrUpdate(Team team)
        {
            throw new System.NotImplementedException();
        }

        public Team GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Team> GetTeamsForProfile(string id)
        {
            return new List<Team>{TestData.CreateTeam1(), TestData.CreateTeam2()};
        }
    }
}