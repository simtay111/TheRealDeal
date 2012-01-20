using System;
using NUnit.Framework;
using RecreateMe.Exceptions;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Teams;

namespace RecreateMe.Tests.Scheduling.Games
{
    [TestFixture]
    public class GameWithTeamTests{

    
        private GameWithTeams _gameWithTeams;

        [SetUp]
        public void SetUp()
        {
            _gameWithTeams = new GameWithTeams(DateTime.Now, null, null);
        }

        [Test]
        public void CanAddTeamsToGame()
        {
            _gameWithTeams.AddTeam(new Team());
            Assert.AreEqual(_gameWithTeams.Teams.Count, 1);
        }

        [Test]
        public void ThrowsCannotAddItemExceptionIfAtCapacity()
        {
            _gameWithTeams.AddTeam(new Team());
            _gameWithTeams.AddTeam(new Team());

            var exception = Assert.Throws(typeof(CannotAddItemException), () => _gameWithTeams.AddTeam(new Team()),
                                          "Exception should have been thrown");
            Assert.That(exception.Message, Is.EqualTo("Could not add team to game, game is full."));
        }
    }
}