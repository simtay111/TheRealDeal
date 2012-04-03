using System;
using System.Collections.Generic;
using RecreateMe.Leagues;

namespace RecreateMe.Organizations
{
    public class Organization
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string CreatorId { get; set; }

        public IList<string> LeagueIds { get; set; }

        public Organization()
        {
            Id = Guid.NewGuid().ToString();
            LeagueIds = new List<string>();
        }
    }
}