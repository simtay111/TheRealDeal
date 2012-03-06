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
        public IList<string> PlayersIds { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string Creator { get; set; }

        public Team()
        {
            if (Id == null)
                Id = Guid.NewGuid().ToString();
            MaxSize = Constants.DefaultTeamSize;
            Name = Constants.DefaultTeamName;
            PlayersIds = new List<string>();
        }

        public Team(string id) : this()
        {
            Id = id;
        }
    }
}