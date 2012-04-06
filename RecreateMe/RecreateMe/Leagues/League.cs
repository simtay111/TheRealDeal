using System;
using System.Collections.Generic;

namespace RecreateMe.Leagues
{
    public class League
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public IList<string> DivisionIds { get; set; }

        public League()
        {
            Id = Guid.NewGuid().ToString();
            DivisionIds = new List<string>();
        }
    }
}