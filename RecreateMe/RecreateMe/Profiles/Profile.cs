using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Sports;
using ServiceStack.DataAnnotations;

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

        public string ProfileId { get; set; }

        [References(typeof(Account))]
        public string AccountName { get; set; }

        [Ignore]
        public IList<string> FriendsIds { get; private set; }

        [Ignore]
        public IList<Location> Locations { get; set; }

        [Ignore]
        public IList<SportWithSkillLevel> SportsPlayed { get; set; }
    }
}