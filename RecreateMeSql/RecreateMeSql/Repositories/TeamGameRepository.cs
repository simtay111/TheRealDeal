using System;
using System.Collections.Generic;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;

namespace RecreateMeSql.Repositories
{
    public class TeamGameRepository : ITeamGameRepository
    {
        public void SaveTeamGame(TeamGame teamGame)
        {
            throw new NotImplementedException();
        }

        public TeamGame GetTeamGameById(string id)
        {
            throw new NotImplementedException();
        }

        public IList<TeamGame> GetTeamGamesForProfile(string profileId)
        {
            throw new NotImplementedException();
        }

        public void AddTeamToGame(string teamid, string gameId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamGame> FindTeamGameByLocation(string location)
        {
            throw new NotImplementedException();
        }

        public void DeleteGame(string id)
        {
            throw new NotImplementedException();
        }

        public IList<TeamGame> GetAllGamesBeforeDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}