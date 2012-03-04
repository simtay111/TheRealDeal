using System.Collections.Generic;
using RecreateMe.Teams;

namespace TheRealDeal.Models.Games
{
    public class AddTeamToGameModel
    {
        public string GameId { get; set; } 
        public string TeamId { get; set; }

        public IList<Team> TeamsForProfile { get; set; }
    }
}