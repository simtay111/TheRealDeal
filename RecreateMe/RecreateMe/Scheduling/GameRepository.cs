using System;
using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling
{
    public class GameRepository : IGameRepository
    {
        public bool SaveOrUpdate(Game game)
        {
            throw new NotImplementedException();
        }

        public Game GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Game> FindByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}