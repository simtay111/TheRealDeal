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

        public PickUpGame(DateTime dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport.Name;
            Location = location.Name;
            Id = Guid.NewGuid().ToString();
            PlayersIds = new List<string>();
        }

        public PickUpGame()
            : this(DateTime.Now, new Sport(), new Location())
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

        [PrimaryKey]
        public string Id { get; set; }

        public DateTime DateTime { get; set; }

        [References(typeof(Location))]
        public string Location { get; set; }

        [References(typeof(Sport))]
        public string Sport { get; set; }

        [References(typeof(Profile))]
        public string Creator { get; set; }

        public bool IsPrivate { get; set; }

        public string ExactLocation { get; set; }

        public int? MaxPlayers { get; set; }

        public int? MinPlayers { get; set; }

        public string GameName { get; set; }
    }
}