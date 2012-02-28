using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Exceptions;
using RecreateMe.Exceptions.Scheduling;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class GameWithoutTeams : Game
    {
        [JsonIgnore]
        public IList<Profile> Players = new List<Profile>();

        public GameWithoutTeams(DateTimeOffset dateTime, Sport sport, Location location) : base(dateTime, sport, location)
        {
        }

        public GameWithoutTeams()
            : base(DateTimeOffset.Now, new Sport(), new Location())
        {
        }

        public void AddPlayer(Profile profile)
        {
            if (Players.Count == MaxPlayers)
            {
                throw new CannotJoinGameException("The game is already at capacity.");
            }
            Players.Add(profile);
        }
    }
}