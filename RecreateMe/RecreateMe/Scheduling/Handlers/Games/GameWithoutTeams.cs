using System;
using System.Collections.Generic;
using RecreateMe.Exceptions;
using RecreateMe.Exceptions.Scheduling;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class GameWithoutTeams : Game
    {
        public IList<Profile> Players = new List<Profile>();

        public GameWithoutTeams(DateTime dateTime, Sport sport, Location location) : base(dateTime, sport, location)
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