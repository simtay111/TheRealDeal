using System.Collections.Generic;
using System.Web.Mvc;
using RecreateMe.Scheduling.Games;

namespace TheRealDeal.Models.Games
{
    public class SearchForGameModel
    {
        public string Location { get; set; }
        public string Sport { get; set; }

        public IList<PickUpGame> Results { get; set; }

        public SelectList SearchableSports { get; set; }
        public SelectList SearchableLocations { get; set; }
    }
}