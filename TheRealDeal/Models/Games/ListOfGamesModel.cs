
using System.Collections.Generic;
using RecreateMe.Scheduling.Games;

namespace TheRealDeal.Models.Games
{
    public class ListOfGamesModel
    {

        public string CurrentProfile { get; set; }

        public IList<TeamGame> TeamGames { get; set; }

        public IList<PickUpGame> PickUpGames { get; set; }
    }
}