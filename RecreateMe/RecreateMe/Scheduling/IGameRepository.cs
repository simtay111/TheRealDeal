using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling
{
    public interface IGameRepository
    {
        bool SaveOrUpdate(Game game);
        Game GetById(string id);
        IList<Game> FindByLocation(string location);
    }
}