using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class GameWithTeams : IAmAGame
    {
        [JsonIgnore]
        public IList<string> TeamsIds = new List<string>();

        public GameWithTeams(DateTimeOffset dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport;
            Location = location;
            Id = Guid.NewGuid().ToString();
        }

        public DateTimeOffset DateTime { get; set; }

        public Sport Sport { get; set; }

        public Location Location { get; set; }

        public int? MinPlayers { get; set; }

        public int? MaxPlayers { get; set; }

        public string Id { get; set; }

        public bool IsPrivate { get; set; }

        public string Creator { get; set; }

        public void AddTeam(string teamId)
        {
            if (IsFull())
                throw new Exception("Could not add team to game, game is full.");
            TeamsIds.Add(teamId);
        }

        public bool IsFull()
        {
            return (TeamsIds.Count == Constants.MaxAmountOfTeamsPerGame);
        }
    }
}