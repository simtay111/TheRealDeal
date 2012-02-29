using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecreateMe.Exceptions;
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
            if (TeamsIds.Count == Constants.MaxAmountOfTeamsPerGame)
                throw new CannotAddItemException("Could not add team to game, game is full.");
            TeamsIds.Add(teamId);
        }
    }
}