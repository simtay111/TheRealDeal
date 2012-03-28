using System;
using System.Collections.Generic;
using RecreateMe.Scheduling.Games;

namespace RecreateMe.Scheduling
{
    public interface ITeamGameRepository
    {
        void SaveTeamGame(TeamGame teamGame);
        TeamGame GetTeamGameById(string id);
        IList<TeamGame> GetTeamGamesForProfile(string profileId);
        void AddTeamToGame(string teamid, string gameId);
        IEnumerable<TeamGame> FindTeamGameByLocation(string location);
        void DeleteGame(string id);
        IList<TeamGame> GetAllGamesBeforeDate(DateTime dateTime);
    }
}