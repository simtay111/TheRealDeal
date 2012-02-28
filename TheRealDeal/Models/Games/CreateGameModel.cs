using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheRealDeal.Models.Games
{
    public class CreateGameModel
    {
        public bool HasTeams { get; set; }

        public bool IsPrivate { get; set; }

        public string Location { get; set; }

        [RegularExpression("[0-100]", ErrorMessage = "Must be a number")]
        public int? MaxPlayers { get; set; }

        [RegularExpression("[0-100]", ErrorMessage = "Must be a number")]
        public int? MinPlayers { get; set; }

        public string Sport { get; set; }

        public IList<string> AvailableSports { get; set; }

        public IList<string> AvailableLocations { get; set; }
    }
}