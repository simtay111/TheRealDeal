using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RecreateMe.Sports;

namespace TheRealDeal.Models.Setup
{
    public class AddSportModel
    {
        [Required(ErrorMessage = "Please choose a sport.")]
        public string ChosenSport { get; set; }
        [Required(ErrorMessage = "Please choose a skill level.")]
        public string ChosenSkillLevel { get; set; }

        public SelectList SkillLevels { get; set; }
        public SelectList AvailableSports { get; set; }
        public IList<SportWithSkillLevel> SportsForProfile { get; set; }
    }
}