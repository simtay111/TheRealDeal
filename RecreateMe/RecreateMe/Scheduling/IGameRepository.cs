using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling
{
    public interface IGameRepository
    {
        bool Save(Game game);
        Game GetById(string id);
        IList<Game> FindByLocation(string location);
        IList<Game> GetForProfile(string profileId);
        void AddPlayerToGame(string gameId, string profileId);
    }
}