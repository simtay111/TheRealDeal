using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using RecreateMe.Locales;
using RecreateMe.Sports;
using RecreateMe.Teams;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class GameWithTeams : Game
    {
        [JsonIgnore]
        public IList<string> TeamsIds = new List<string>();

        public GameWithTeams(DateTimeOffset dateTime, Sport sport, Location location) : base(dateTime, sport, location)
        {
            HasTeams = true;
        }

        public void AddTeam(string teamId)
        {
            if (IsFull())
                throw new Exception("Could not add team to game, game is full.");
            TeamsIds.Add(teamId);
        }

        public override bool IsFull()
        {
            return (TeamsIds.Count == Constants.MaxAmountOfTeamsPerGame);
        }
    }
}