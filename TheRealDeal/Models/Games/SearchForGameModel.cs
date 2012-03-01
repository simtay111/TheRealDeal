using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDeal.Models.Games
{
    public class SearchForGameModel
    {
        public string Location { get; set; }
        public string Sport { get; set; }

        public IList<Game> Results { get; set; }

    }
}