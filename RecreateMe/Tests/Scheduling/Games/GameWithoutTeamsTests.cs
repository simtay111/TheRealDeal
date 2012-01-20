using System;
using NUnit.Framework;
using RecreateMe.Exceptions;
using RecreateMe.Exceptions.Scheduling;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Tests.Scheduling.Handlers;

namespace RecreateMe.Tests.Scheduling.Games
{
    [TestFixture]
    public class GameWithoutTeamsTests
    {
         [Test]
        public void CannotAddPlayerToGameIfAtMaxCapacity()
         {
             var game = new GameWithoutTeams(DateTime.Now, null, null);
             game.MaxPlayers = 0;

             var exception = Assert.Throws(typeof (CannotJoinGameException), () => game.AddPlayer(new Profile()));
             Assert.AreEqual(exception.Message, "The game is already at capacity.");
         }
    }
}