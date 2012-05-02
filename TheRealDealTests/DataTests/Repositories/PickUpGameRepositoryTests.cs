using System;
using System.Linq;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class PickUpGameRepositoryTests
    {
        private PickUpGameRepository _repo;
        private SampleDataBuilder _data;

        [SetUp]
        public void SetUp()
        {
            _repo = new PickUpGameRepository();
            _data = new SampleDataBuilder();
            _data.DeleteAllData();
        }

        [Test]
        public void CanSaveAndGetPickupGames()
        {
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();
            _data.CreateLocationBend();
            _data.CreateSoccerSport();

            var game = new PickUpGame(DateTimeOffset.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = new Sport("Soccer");
            game.Location = new Location("Bend");
            game.AddPlayer(profile.ProfileId);
            game.Creator = profile.ProfileId;
            game.ExactLocation = "A road in space";

            _repo.SavePickUpGame(game);

            var retrievedGame = _repo.GetPickUpGameById(game.Id);

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
            Assert.That(retrievedGame.Location.Name, Is.EqualTo(game.Location.Name));
            Assert.That(retrievedGame.Sport.Name, Is.EqualTo(game.Sport.Name));
            Assert.That(retrievedGame.IsPrivate, Is.EqualTo(game.IsPrivate));
            Assert.That(retrievedGame.MinPlayers, Is.EqualTo(game.MinPlayers));
            Assert.That(retrievedGame.MaxPlayers, Is.EqualTo(game.MaxPlayers));
            Assert.That(retrievedGame.DateTime, Is.InRange(game.DateTime.AddSeconds(-1), game.DateTime.AddSeconds(1)));
            Assert.That(retrievedGame.PlayersIds[0], Is.EqualTo(profile.ProfileId));
            Assert.That(retrievedGame.Creator, Is.EqualTo(game.Creator));
            Assert.That(retrievedGame.ExactLocation, Is.EqualTo(game.ExactLocation));
        }

        [Test]
        public void CanGetListOfGamesThatProfileIsPartOf()
        {
            _data.CreateData();

            var pickUpGames = _repo.GetPickupGamesForProfile("Simtay111");

            Assert.That(pickUpGames.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanGetPickUpGameByLocation()
        {
            _data.CreateData();

            var games = _repo.FindPickUpGameByLocation("Bend");

            Assert.That(games.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanAddProfilesToGame()
        {
            _data.CreateData();

            const string profileId = "Profile1";
            _repo.AddPlayerToGame(_data.PickUpGame.Id, profileId);

            var game = _repo.GetPickUpGameById(_data.PickUpGame.Id);

            Assert.That(game.PlayersIds.Any(x => x == profileId));
        }

        [Test]
        public void CanDeletePickUpGames()
        {
            _data.CreateData();

            _repo.DeleteGame(_data.PickUpGame.Id);

            var exception = Assert.Throws<InvalidOperationException>(() => _repo.GetPickUpGameById(_data.PickUpGame.Id));
            Assert.That(exception.Message, Is.EqualTo("Sequence contains no elements"));
        }

        [Test]
        public void DoesNotThrowIfGameDoesNotExist()
        {
            _data.CreateAccounts();
            _repo.CreateGameBaseNode();

            Assert.DoesNotThrow(() => _repo.DeleteGame("123"));
        }
    }
}