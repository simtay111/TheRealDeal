using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using ServiceStack.DataAnnotations;

namespace RecreateMe.Scheduling.Games
{
    public class PickUpGame : IAmAGame
    {
        [Ignore]
        public IList<string> PlayersIds { get; set; }

        public PickUpGame(DateTimeOffset dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport;
            Location = location;
            Id = Guid.NewGuid().ToString();
            PlayersIds = new List<string>();
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
        [Ignore]
        public Location Location { get; set; }
        [Ignore]
        public Sport Sport { get; set; }

        public DateTimeOffset DateTime { get; set; }

        [PrimaryKey]
        public string Id { get; set; }
        
        [References(typeof(Profile))]
        public string Creator { get; set; }

        public bool IsPrivate { get; set; }

        public string ExactLocation { get; set; }
    }
}