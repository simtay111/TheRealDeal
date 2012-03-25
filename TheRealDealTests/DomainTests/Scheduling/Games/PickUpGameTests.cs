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
    public class PickUpGameTests
    {
        private PickUpGame _game;

        [SetUp]
        public void SetUp()
        {
            _game = new PickUpGame(DateTime.Now, null, null);
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
            Assert.That(_game.MinPlayers, Is.EqualTo(3));
            Assert.That(_game.MaxPlayers, Is.EqualTo(5));
        }

        [Test]
        public void EveryGameHasAGameIdThatIsAGuidCreatedAtCreationgOfGame()
        {
            Assert.NotNull(_game.Id);
        }

         [Test]
        public void CannotAddPlayerToGameIfAtMaxCapacity()
         {
             var game = new PickUpGame(DateTime.Now, null, null);
             game.MaxPlayers = 0;

             var exception = Assert.Throws(typeof (Exception), () => game.AddPlayer("Profile1"));
             Assert.AreEqual(exception.Message, "The game is already at capacity.");
         }

        [Test]
        public void CanCheckToSeeIfGameIsFull()
        {
            var game = new PickUpGame(DateTime.Now, null, null);
            game.MaxPlayers = 0;

            Assert.True(game.IsFull());
        }
    }
}