using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Games
{
    public interface IGameFactory
    {
        TeamGame CreateGameWithTeams(DateTime dateTime, Sport sport, Location location, bool isPrivate = false);
        PickUpGame CreatePickUpGame(DateTime dateTime, Sport sport, Location location, bool isPrivate = false);
    }
}