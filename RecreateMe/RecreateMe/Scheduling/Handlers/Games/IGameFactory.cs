using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Games
{
    public interface IGameFactory
    {
        GameWithTeams CreateGameWithTeams(DateTime dateTime, Sport sport, Location location, bool isPrivate = false);
        GameWithoutTeams CreateGameWithOutTeams(DateTime dateTime, Sport sport, Location location, bool isPrivate = false);
    }
}