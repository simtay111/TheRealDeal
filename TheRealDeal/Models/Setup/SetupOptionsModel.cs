using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace TheRealDeal.Models.Setup
{
    public class SetupOptionsModel
    {
        public IList<SportWithSkillLevel> SportsForProfile { get; set; }

        public IList<Location> CurrentLocations { get; set; }

        public SelectList SkillLevels { get; set; }

        public SelectList AvailableSports { get; set; }

        public SelectList AvailableLocations { get; set; }

        public string LocationToAdd { get; set; }

        public string ChosenSkillLevel { get; set; }

        public string ChosenSport { get; set; }
    }
}