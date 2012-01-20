using System;
using System.Collections.Generic;
using RecreateMe.Exceptions;
using RecreateMe.Locales;
using RecreateMe.Sports;
using RecreateMe.Teams;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class GameWithTeams : Game
    {
        public IList<Team> Teams = new List<Team>();

        public GameWithTeams(DateTime dateTime, Sport sport, Location location) : base(dateTime, sport, location)
        {
        }

        public void AddTeam(Team team)
        {
            if (Teams.Count == Constants.MaxAmountOfTeamsPerGame)
                throw new CannotAddItemException("Could not add team to game, game is full.");
            Teams.Add(team);
        }
    }
}