using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public class GameFactory : IGameFactory
    {
        public GameWithTeams CreateGameWithTeams(DateTime dateTime, Sport sport, Location location, bool isPrivate = false)
        {
            return new GameWithTeams(dateTime, sport, location) {IsPrivate = isPrivate};
        }

        public PickUpGame CreatePickUpGame(DateTime dateTime, Sport sport, Location location, bool isPrivate = false)
        {
            return new PickUpGame(dateTime, sport, location) { IsPrivate = isPrivate };
        }
    }
}