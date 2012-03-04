using System;
using NUnit.Framework;
using RecreateMe.Exceptions;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Teams;

namespace TheRealDealTests.DomainTests.Scheduling.Games
{
    [TestFixture]
    public class GameWithTeamsTests{

    
        private GameWithTeams _gameWithTeams;

        [SetUp]
        public void SetUp()
        {
            _gameWithTeams = new GameWithTeams(DateTime.Now, null, null);
        }

        [Test]
        public void CanAddTeamsToGame()
        {
            _gameWithTeams.AddTeam("team1");
            Assert.AreEqual(_gameWithTeams.TeamsIds.Count, 1);
        }

        [Test]
        public void ThrowsCannotAddItemExceptionIfAtCapacity()
        {
            _gameWithTeams.AddTeam("Team1");
            _gameWithTeams.AddTeam("Team2");

            var exception = Assert.Throws(typeof(CannotAddItemException), (TestDelegate) (() => _gameWithTeams.AddTeam("Team3")),
                                          "Exception should have been thrown");
            Assert.That(exception.Message, Is.EqualTo("Could not add team to game, game is full."));
        }

        [Test]
        public void SetsBaseGameTypeWhenCreatedToNoTeams()
        {
            var game = new GameWithTeams(DateTime.Now, null, null);

            var baseGame = game as Game;

            Assert.That(baseGame.HasTeams, Is.True);
        }

        [Test]
        public void CanCheckIfGameIsFull()
        {
            var game = new GameWithTeams(DateTime.Now, null, null);
            game.TeamsIds.Add("123");
            game.TeamsIds.Add("123");

            Assert.True(game.IsFull());
        }
    }
}