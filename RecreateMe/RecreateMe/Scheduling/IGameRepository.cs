using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling
{
    public interface IGameRepository
    {
        bool Save(Game game);
        void SavePickUpGame(PickUpGame game);
        void SaveTeamGame(GameWithTeams game);
        GameWithTeams GetTeamGameById(string id);
        PickUpGame GetPickUpGameById(string id);
        IList<PickUpGame> FindPickUpGameByLocation(string location);
        IList<PickUpGame> GetPickupGamesForProfile(string profileId);
        IList<GameWithTeams> GetTeamGamesForProfile(string profileId);
        void AddPlayerToGame(string gameId, string profileId);
        void AddTeamToGame(string teamid, string gameId);
    }
}