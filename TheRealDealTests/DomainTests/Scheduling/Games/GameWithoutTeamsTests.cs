using System;
using NUnit.Framework;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDealTests.DomainTests.Scheduling.Games
{
    [TestFixture]
    public class GameWithoutTeamsTests
    {
         [Test]
        public void CannotAddPlayerToGameIfAtMaxCapacity()
         {
             var game = new GameWithoutTeams(DateTime.Now, null, null);
             game.MaxPlayers = 0;

             var exception = Assert.Throws(typeof (Exception), () => game.AddPlayer("Profile1"));
             Assert.AreEqual(exception.Message, "The game is already at capacity.");
         }

        [Test]
        public void SetsBaseGameTypeWhenCreatedToNoTeams()
        {
            var game = new GameWithoutTeams(DateTime.Now, null, null);

            var baseGame = game as Game;

            Assert.That(baseGame.HasTeams, Is.False);
        }

        [Test]
        public void CanCheckToSeeIfGameIsFull()
        {
            var game = new GameWithoutTeams(DateTime.Now, null, null);
            game.MaxPlayers = 0;

            Assert.True(game.IsFull());
        }
    }
}