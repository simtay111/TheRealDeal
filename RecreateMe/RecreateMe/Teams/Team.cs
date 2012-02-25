using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Profiles;

namespace RecreateMe.Teams
{
    public class Team
    {
        public string Id { get; private set; }
        public int? MaxSize { get; set; }
        [JsonIgnore]
        public IList<Profile> Players { get; set; }
        public string Name { get; set; }

        public Team()
        {
            if (Id == null)
                Id = Guid.NewGuid().ToString();
            MaxSize = Constants.DefaultTeamSize;
            Name = Constants.DefaultTeamName;
            Players = new List<Profile>();
        }

        public Team(string id) : this()
        {
            Id = id;
        }
    }
}