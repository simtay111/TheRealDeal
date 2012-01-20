using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class Game
    {
        public readonly string Id;
        public DateTime DateTime { get; set; }
        public Sport Sport { get; set; }
        public Location Location { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public bool IsPrivate { get; set; }

        public Game(DateTime dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport;
            Location = location;
            Id = Guid.NewGuid().ToString();
        }
    }
}