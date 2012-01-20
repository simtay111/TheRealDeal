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

        public GameWithoutTeams CreateGameWithOutTeams(DateTime dateTime, Sport sport, Location location, bool isPrivate = false)
        {
            return new GameWithoutTeams(dateTime, sport, location) { IsPrivate = isPrivate };
        }
    }
}