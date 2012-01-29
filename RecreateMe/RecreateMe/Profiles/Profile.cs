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
            FriendsIds = new List<string>();
        }

        public string AccountId { get; set; }
        public IList<string> FriendsIds;
        public Name Name { get; set; }
        public string FullAccountName { get { return String.Format("{0} ({1})", Name.FullName, AccountId); }}
        public IList<Location> Locations { get; set; }
        public string UniqueId { get; set; }
        public IList<SportWithSkillLevel> SportsPlayed { get; set; }

    }
}