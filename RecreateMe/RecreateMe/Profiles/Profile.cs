using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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

        [JsonIgnore]
        public string AccountId { get; set; }

        [JsonIgnore]
        public IList<string> FriendsIds;

        [JsonIgnore]
        public IList<Location> Locations { get; set; }

        [JsonIgnore]
        public IList<SportWithSkillLevel> SportsPlayed { get; set; }

        public string ProfileId { get; set; }
    }
}