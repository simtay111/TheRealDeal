using System;
using System.Collections.Generic;
using RecreateMe.Profiles;

namespace RecreateMe.Teams
{
    public class Team
    {
        public readonly string Id;
        public int? MaxSize { get; set; }
        public IList<Profile> Players { get; set; }
        public string Name { get; set; }

        public Team()
        {
            Id = Guid.NewGuid().ToString();
            MaxSize = Constants.DefaultTeamSize;
            Name = Constants.DefaultTeamName;
            Players = new List<Profile>();
        }
    }
}