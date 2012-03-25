using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class PickUpGame : IAmAGame
    {
        [JsonIgnore]
        public IList<string> PlayersIds = new List<string>();

        public PickUpGame(DateTimeOffset dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport;
            Location = location;
            Id = Guid.NewGuid().ToString();
        }

        public PickUpGame() :this(DateTimeOffset.Now, null, null)
        {
        }

        public void AddPlayer(string profileId)
        {
            if (IsFull())
            {
                throw new Exception("The game is already at capacity.");
            }
            PlayersIds.Add(profileId);
        }

        public bool IsFull()
        {
            return (PlayersIds.Count >= MaxPlayers);
        }

        public int? MaxPlayers { get; set; }

        public int? MinPlayers { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }
        [JsonIgnore]
        public Sport Sport { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string Id { get; set; }
        [JsonIgnore]
        public string Creator { get; set; }

        public bool IsPrivate { get; set; }
    }
}