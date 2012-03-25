using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling
{
    public interface ITeamGameRepository
    {
        void SaveTeamGame(GameWithTeams game);
        GameWithTeams GetTeamGameById(string id);
        IList<GameWithTeams> GetTeamGamesForProfile(string profileId);
        void AddTeamToGame(string teamid, string gameId);
        void CreateGameBaseNode(string sportName);
    }
}