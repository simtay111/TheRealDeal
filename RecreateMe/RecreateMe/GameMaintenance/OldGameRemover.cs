using System;
using RecreateMe.Configuration;
using RecreateMe.Scheduling;

namespace RecreateMe.GameMaintenance
{
    public class OldGameRemover : IOldGameRemover
    {
        private readonly IPickUpGameRepository _gameRepo;

        public OldGameRemover(IPickUpGameRepository gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public void CleanForPastMinutes(int minutes)
        {
            var end = DateTime.Now;
            var start = end.AddMinutes(-minutes);
            var games = _gameRepo.GetGamesWithinDateRange(start, end);

            games.ForEach(x => _gameRepo.DeleteGame(x.Id));
        }
    }

    public interface IOldGameRemover
    {
    }
}