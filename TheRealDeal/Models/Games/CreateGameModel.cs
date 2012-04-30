using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheRealDeal.Models.Games
{
    public class CreateGameModel
    {
        public string Location { get; set; }

        [RegularExpression("[0-9]{0,2}", ErrorMessage = "Must be a number")]
        public int? MaxPlayers { get; set; }

        [RegularExpression("[0-9]{0,2}", ErrorMessage = "Must be a number")]
        public int? MinPlayers { get; set; }

        public string Sport { get; set; }

        public IList<string> AvailableSports { get; set; }

        public IList<string> AvailableLocations { get; set; }

        [RegularExpression(@"^(([1-9])|(0[1-9])|(1[0-2]))\/(([0-9])|([0-2][0-9])|(3[0-1]))\/(([0-9][0-9])|([1-2][0,9][0-9][0-9]))$", ErrorMessage = "Please Use Format: 04/23/1987")]
        [Required(ErrorMessage = "Please specify a date for the game.")]
        public string DateOfEvent{ get; set; }

        [RegularExpression(@"^(([0-9])|([0-1][0-9])|([2][0-3])):(([0-9])|([0-5][0-9]))$", ErrorMessage = "Please Use Format: 07:30 or 18:30 (6:30 PM)")]
        [Required(ErrorMessage = "Please specify a time for the game.")]
        public string TimeOfEvent { get; set; }

        [Required(ErrorMessage = "You must specify a specific location where your game will occur")]
        public string ExactLocation { get; set; }
    }
}