using System.Collections.Generic;
using RecreateMe.Scheduling.Games;

namespace TheRealDeal.Models.Games
{
    public class ViewGameModel
    {
        public PickUpGame Game { get; set; }

        public string ProfileId { get; set; }

        public Dictionary<string, int> PlayerIdsAndSkillLevels { get; set; }
    }
}