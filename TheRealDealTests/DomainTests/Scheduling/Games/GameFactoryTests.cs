using System;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Scheduling.Games
{
    [TestFixture]
    public class GameFactoryTests
    {
        private GameFactory _factory;
        private Location _location;
        private Sport _sport;

        [SetUp]
        public void SetUp()
        {
            _factory = new GameFactory();
            _sport = new Sport("Soccer");
            _location = new Location("Bend");
        }

        [Test]
        public void CanCreateGameWithTeams()
        {
            GameWithTeams game = _factory.CreateGameWithTeams(DateTime.Now, _sport, _location);

            AssertGameWithTeamsWasCreatedAndDataLinesUp(game);
        }

        [Test]
        public void CanCreateGameWithoutTeams()
        {
            PickUpGame pickUpGame = _factory.CreatePickUpGame(DateTime.Now, _sport, _location);

            AssertGameWithTeamsWasCreatedAndDataLinesUp(pickUpGame);
        }

        [Test]
        public void CanCreateAGameThatIsPrivate()
        {
            const bool isPrivate = true;
            var game = _factory.CreatePickUpGame(DateTime.Now, _sport, _location, isPrivate);
            Assert.That(game.IsPrivate, Is.True);
            var teamGame = _factory.CreateGameWithTeams(DateTime.Now, _sport, _location, isPrivate);
            Assert.That(teamGame.IsPrivate, Is.True);
        }

        private void AssertGameWithTeamsWasCreatedAndDataLinesUp(GameWithTeams game)
        {
            Assert.NotNull(game);
            Assert.That(game.Sport.Name, Is.EqualTo("Soccer"));
            Assert.That(game.Location.Name, Is.EqualTo(_location.Name));
        }


        private void AssertGameWithTeamsWasCreatedAndDataLinesUp(PickUpGame game)
        {
            Assert.NotNull(game);
            Assert.That(game.Sport.Name, Is.EqualTo("Soccer"));
            Assert.That(game.Location.Name, Is.EqualTo(_location.Name));
        }
    }
}