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
    public class GameTests
    {
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            _game = new TestableGame(DateTime.Now, null, null);
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
    }

    public class TestableGame : Game
    {
        public TestableGame(DateTime dateTime, Sport sport, Location location) : base(dateTime, sport, location)
        {
        }

        public override bool IsFull()
        {
            throw new NotImplementedException();
        }
    }
}