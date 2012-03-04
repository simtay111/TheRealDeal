using System;
using Newtonsoft.Json;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public abstract class Game : IGame
    {
        public string Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        [JsonIgnore]
        public Sport Sport { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }


        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }

        public bool IsPrivate { get; set; }
        public bool HasTeams { get; set; }

        public Game(DateTimeOffset dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport;
            Location = location;
            Id = Guid.NewGuid().ToString();
        }

        public Game() : this(DateTimeOffset.Now, null, null){}

        public abstract bool IsFull();

    }
}