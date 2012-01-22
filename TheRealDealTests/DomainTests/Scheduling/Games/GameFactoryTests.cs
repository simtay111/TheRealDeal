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
            _location = new Location(1);
        }

        [Test]
        public void CanCreateGameWithTeams()
        {
            GameWithTeams game = _factory.CreateGameWithTeams(DateTime.Now, _sport, _location);

            AssertGameWasCreatedAndDataLinesUp(game);
        }

        [Test]
        public void CanCreateGameWithoutTeams()
        {
            GameWithoutTeams game = _factory.CreateGameWithOutTeams(DateTime.Now, _sport, _location);

            AssertGameWasCreatedAndDataLinesUp(game);
        }

        [Test]
        public void CanCreateAGameThatIsPrivate()
        {
            const bool isPrivate = true;
            Game game = _factory.CreateGameWithOutTeams(DateTime.Now, _sport, _location, isPrivate);
            Assert.That((object) game.IsPrivate, Is.True);
            game = _factory.CreateGameWithTeams(DateTime.Now, _sport, _location, isPrivate);
            Assert.That((object) game.IsPrivate, Is.True);

        }

        private static void AssertGameWasCreatedAndDataLinesUp(Game game)
        {
            Assert.NotNull(game);
            Assert.That((object) game.Sport.Name, Is.EqualTo("Soccer"));
            Assert.That((object) game.Location.Id, Is.EqualTo(1));
        }
    }
}