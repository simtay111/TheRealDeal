using System;
using System.Collections.Generic;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public class Profile
    {
        public Profile()
        {
            Locations = new List<Location>();
            SportsPlayed = new List<SportWithSkillLevel>();
            Friends = new List<string>();
        }

        public string UserId { get; set; }
        public IList<string> Friends;
        public Name Name { get; set; }
        public IList<Location> Locations { get; set; }
        public string UniqueId { get; set; }
        public IList<SportWithSkillLevel> SportsPlayed { get; set; }
    }
}