using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Games
{
    public class TeamGame : IAmAGame
    {
        [JsonIgnore]
        public IList<string> TeamsIds = new List<string>();

        public TeamGame(DateTime dateTime, Sport sport, Location location)
        {
            DateTime = dateTime;
            Sport = sport.Name;
            Location = location.Name;
            Id = Guid.NewGuid().ToString();
        }

        public DateTime DateTime { get; set; }
        [JsonIgnore]
        public string Sport { get; set; }
        [JsonIgnore]
        public string Location { get; set; }

        public int? MinPlayers { get; set; }

        public int? MaxPlayers { get; set; }

        public string Id { get; set; }

        public bool IsPrivate { get; set; }
        [JsonIgnore]
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