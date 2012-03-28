using System.Collections.Generic;
using RecreateMe.Scheduling.Games;

namespace RecreateMe.Scheduling
{
    public interface IPickUpGameRepository
    {
        void SavePickUpGame(PickUpGame game);
        PickUpGame GetPickUpGameById(string id);
        IList<PickUpGame> FindPickUpGameByLocation(string location);
        IList<PickUpGame> GetPickupGamesForProfile(string profileId);
        void AddPlayerToGame(string gameId, string profileId);
        void DeleteGame(string gameId);
    }
}