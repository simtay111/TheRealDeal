using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMeSql;
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
        private Profile _profile;

        [SetUp]
        public void SetUp()
        {
            _repo = new PickUpGameRepository();
            _data = new SampleDataBuilder();
            SqlServerDataHelper.DeleteAllData();
        }

        [Test]
        public void CanSaveAndGetPickupGames()
        {
            var game = CreateGame();

            var retrievedGame = _repo.GetPickUpGameById(game.Id);

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
            Assert.That(retrievedGame.Location, Is.EqualTo(game.Location));
            Assert.That(retrievedGame.Sport, Is.EqualTo(game.Sport));
            Assert.That(retrievedGame.IsPrivate, Is.EqualTo(game.IsPrivate));
            Assert.That(retrievedGame.MinPlayers, Is.EqualTo(game.MinPlayers));
            Assert.That(retrievedGame.MaxPlayers, Is.EqualTo(game.MaxPlayers));
            Assert.That(retrievedGame.DateTime, Is.InRange(game.DateTime.AddSeconds(-1), game.DateTime.AddSeconds(1)));
            Assert.That(retrievedGame.PlayersIds[0], Is.EqualTo(_profile.ProfileName));
            Assert.That(retrievedGame.Creator, Is.EqualTo(game.Creator));
            Assert.That(retrievedGame.ExactLocation, Is.EqualTo(game.ExactLocation));
            Assert.That(retrievedGame.GameName, Is.EqualTo(game.GameName));
        }

        [Test]
        public void CanGetListOfGamesThatProfileIsPartOf()
        {
            CreateGame();
            CreatePickUpGameOnly();

            var pickUpGames = _repo.GetPickupGamesForProfile("Simtay111");

            Assert.That(pickUpGames.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetListOfGamesThatProfileCreated()
        {
            CreateGame();
            CreatePickUpGameOnly();

            var createdGames =  _repo.GetByCreated(SampleDataBuilder.Profile1Id);
            Assert.That(createdGames.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetPickUpGameByLocation()
        {
            var game = CreateGame();
            CreatePickUpGameOnly();

            var games = _repo.FindPickUpGameByLocation("Bend");

            Assert.That(games.Count, Is.EqualTo(2));
            Assert.That(games[0].PlayersIds, Has.Member(game.PlayersIds[0]));
        }

        [Test]
        public void CanAddProfilesToGame()
        {
            var game = CreateGame();
            _data.CreateAccountWithProfile2();


            const string profileId = "Profile1";
            _repo.AddPlayerToGame(game.Id, profileId);

            var retrievedGame = _repo.GetPickUpGameById(game.Id);

            Assert.That(retrievedGame.PlayersIds.Any(x => x == profileId));
        }

        [Test]
        public void CanRemoveProfilesFromGame()
        {
            var game = CreateGame();
            _data.CreateAccountWithProfile2();
            _repo.AddPlayerToGame(game.Id, "Profile1");

            const string profileId = "Profile1";
            _repo.RemovePlayerFromGame(profileId, game.Id);

            var retrievedGame = _repo.GetPickUpGameById(game.Id);

            Assert.True(!retrievedGame.PlayersIds.Any(x => x == profileId));
        }

        [Test]
        public void CanDeletePickUpGames()
        {
            var game = CreateGame();

            _repo.DeleteGame(game.Id);

            Assert.Throws<ArgumentNullException>(() => _repo.GetPickUpGameById(game.Id));
        }

        [Test]
        public void DoesNotThrowIfGameDoesNotExist()
        {
            Assert.DoesNotThrow(() => _repo.DeleteGame("123"));
        }

        [Test]
        public void CanGetAllGamesWithinADateRange()
        {
            var game = CreateGame();

            
        }

        private PickUpGame CreateGame()
        {
            _data.CreateLocationBend();
            _data.CreateSoccerSport();
            _data.CreateBasketballSport();
            _data.CreateAccount1();
            _profile = _data.CreateProfileForAccount1();


            var game = CreatePickUpGameOnly();
            return game;
        }

        private PickUpGame CreatePickUpGameOnly()
        {
            var game = new PickUpGame(DateTime.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = "Soccer";
            game.Location = "Bend";
            game.AddPlayer(_profile.ProfileName);
            game.Creator = _profile.ProfileName;
            game.ExactLocation = "A road in space";
            game.GameName = "A game name";

            _repo.SavePickUpGame(game);
            return game;
        }
    }
}