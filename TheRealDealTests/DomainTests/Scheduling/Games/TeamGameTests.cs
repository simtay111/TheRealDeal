using System;
using System.Diagnostics;
using Moq;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Scheduling.Games
{
    [TestFixture]
    public class TeamGameTests
    {
        private TeamGame _teamGame;

        [SetUp]
        public void SetUp()
        {
            _teamGame = new TeamGame(DateTime.Now, new Sport(), new Location());
        }

        [Test]
        public void KnowsItsCreator()
        {
            _teamGame.Creator = "123";
            Assert.That(_teamGame.Creator, Is.EqualTo("123"));
        }

        [Test]
        public void KnowsIfItIsPrivate()
        {
            _teamGame.IsPrivate = false;
            Assert.False(_teamGame.IsPrivate);
        }

        [Test]
        public void HasDateTimeForEvent()
        {
            Assert.NotNull(_teamGame.DateTime);
            Trace.Write(_teamGame.DateTime);
        }

        [Test]
        public void HasASport()
        {
            var sport = new Sport("Soccer");
            _teamGame.Sport = sport.Name;
        }

        [Test]
        public void HasALocation()
        {
            var location = new Location("Bend");
            _teamGame.Location = location.Name;
        }

        [Test]
        public void HoldsMinAndMaximumPlayerValues()
        {
            Assert.Null(_teamGame.MinPlayers);
            Assert.Null(_teamGame.MaxPlayers);
            _teamGame.MinPlayers = 3;
            _teamGame.MaxPlayers = 5;
            Assert.NotNull(_teamGame.MinPlayers);
            Assert.NotNull(_teamGame.MaxPlayers);
        }

        [Test]
        public void EveryGameHasAGameIdThatIsAGuidCreatedAtCreationgOfGame()
        {
            Assert.NotNull(_teamGame.Id);
        }

        [Test]
        public void CanAddTeamsToGame()
        {
            _teamGame.AddTeam("team1");
            Assert.AreEqual(_teamGame.TeamsIds.Count, 1);
        }

        [Test]
        public void ThrowsCannotAddItemExceptionIfAtCapacity()
        {
            _teamGame.AddTeam("Team1");
            _teamGame.AddTeam("Team2");

            var exception = Assert.Throws(typeof(Exception), () => _teamGame.AddTeam("Team3"),
                                          "Exception should have been thrown");
            Assert.That(exception.Message, Is.EqualTo("Could not add team to game, game is full."));
        }

        [Test]
        public void CanCheckIfGameIsFull()
        {
            var game = new TeamGame(DateTime.Now, new Sport(), new Location());
            game.TeamsIds.Add("123");
            game.TeamsIds.Add("123");

            Assert.True(game.IsFull());
        }
    }
}