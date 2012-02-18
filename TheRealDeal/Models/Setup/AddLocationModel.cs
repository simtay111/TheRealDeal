using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecreateMe.Locales;

namespace TheRealDeal.Models.Setup
{
    public class AddLocationModel
    {
        [Required(ErrorMessage = "You must specify a location.")]
        public string LocationToAdd { get; set; }

        public IList<Location> CurrentLocations { get; set; }
    }
}