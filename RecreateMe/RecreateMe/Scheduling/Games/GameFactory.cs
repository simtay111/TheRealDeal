using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Games
{
    public class GameFactory : IGameFactory
    {
        public TeamGame CreateGameWithTeams(DateTime dateTime, Sport sport, Location location, bool isPrivate = false)
        {
            return new TeamGame(dateTime, sport, location) {IsPrivate = isPrivate};
        }

        public PickUpGame CreatePickUpGame(DateTime dateTime, Sport sport, Location location, bool isPrivate = false)
        {
            return new PickUpGame(dateTime, sport, location) { IsPrivate = isPrivate };
        }
    }
}