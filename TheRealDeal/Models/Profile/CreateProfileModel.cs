using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheRealDeal.Models.Profile
{
    public class CreateProfileModel
    {
        [Required(ErrorMessage = "You must specify a Name!")]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Sports { get; set; }

        [RegularExpression("[0-9]", ErrorMessage = "Skill Level Must Be From One To Ten!")]
        public string SkillLevel { get; set; }

        public SelectList AvailableSports { get; set; }
        public SelectList AvailableLocations { get; set; }
        public SelectList AvailableSkillLevels { get; set; }
    }
}