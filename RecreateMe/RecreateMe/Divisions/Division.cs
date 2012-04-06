using System;
using System.Collections.Generic;

namespace RecreateMe.Divisions
{
    public class Division
    {
        public Division()
        {
            Id = Guid.NewGuid().ToString();
            TeamIds = new List<string>();
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public List<string> TeamIds { get; set; }
    }
}