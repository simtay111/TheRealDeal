using System;
using System.Diagnostics;
using Moq;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Scheduling.Games
{
    [TestFixture]
    public class GameWithTeamsTests
    {
        private GameWithTeams _game;

        [SetUp]
        public void SetUp()
        {
            _game = new GameWithTeams(DateTime.Now, null, null);
        }

        [Test]
        public void KnowsItsCreator()
        {
            _game.Creator = "123";
            Assert.That(_game.Creator, Is.EqualTo("123"));
        }

        [Test]
        public void KnowsIfItIsPrivate()
        {
            _game.IsPrivate = false;
            Assert.False(_game.IsPrivate);
        }

        [Test]
        public void HasDateTimeForEvent()
        {
            Assert.NotNull(_game.DateTime);
            Trace.Write(_game.DateTime);
        }

        [Test]
        public void HasASport()
        {
            var sport = new Mock<Sport>().Object;
            _game.Sport = sport;
            Assert.That(_game.Sport, Is.InstanceOf<Sport>());
        }

        [Test]
        public void HasALocation()
        {
            var location = new Mock<Location>().Object;
            _game.Location = location;
            Assert.That(_game.Location, Is.InstanceOf<Location>());
        }

        [Test]
        public void HoldsMinAndMaximumPlayerValues()
        {
            Assert.Null(_game.MinPlayers);
            Assert.Null(_game.MaxPlayers);
            _game.MinPlayers = 3;
            _game.MaxPlayers = 5;
            Assert.NotNull(_game.MinPlayers);
            Assert.NotNull(_game.MaxPlayers);
        }

        [Test]
        public void EveryGameHasAGameIdThatIsAGuidCreatedAtCreationgOfGame()
        {
            Assert.NotNull(_game.Id);
        }

        [Test]
        public void CanAddTeamsToGame()
        {
            _game.AddTeam("team1");
            Assert.AreEqual(_game.TeamsIds.Count, 1);
        }

        [Test]
        public void ThrowsCannotAddItemExceptionIfAtCapacity()
        {
            _game.AddTeam("Team1");
            _game.AddTeam("Team2");

            var exception = Assert.Throws(typeof(Exception), () => _game.AddTeam("Team3"),
                                          "Exception should have been thrown");
            Assert.That(exception.Message, Is.EqualTo("Could not add team to game, game is full."));
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