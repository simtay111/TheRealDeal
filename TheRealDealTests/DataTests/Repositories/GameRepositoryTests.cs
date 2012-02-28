using System;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    public class GameRepositoryTests
    {
        private GameRepository _repo;
        private SampleDataBuilder _data;

        [SetUp]
        public void SetUp()
        {
            _repo = new GameRepository();
            _data = new SampleDataBuilder();
        }

        [Test]
        public void CanSave()
        {
            var game = new GameWithoutTeams(DateTime.Now, new Sport(), new Location());

            _repo.SaveOrUpdate(game);

            var retrievedGame = _repo.GetById(game.Id);

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
        }

        [Test]
        public void CanGetById()
        {
            _data.DeleteAllData();
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();
            _data.CreateLocationBend();
            _data.CreateSoccerSport();

            var game = new GameWithoutTeams(DateTimeOffset.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = new Sport("Soccer");
            game.Location = new Location("Bend");
            game.AddPlayer(profile);


            _repo.SaveOrUpdate(game);

            var retrievedGame = _repo.GetById(game.Id);

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
            Assert.That(retrievedGame.Location.Name, Is.EqualTo(game.Location.Name));
            Assert.That(retrievedGame.Sport.Name, Is.EqualTo(game.Sport.Name));
            Assert.That(retrievedGame.IsPrivate, Is.EqualTo(game.IsPrivate));
            Assert.That(retrievedGame.MinPlayers, Is.EqualTo(game.MinPlayers));
            Assert.That(retrievedGame.MaxPlayers, Is.EqualTo(game.MaxPlayers));
            Assert.That(retrievedGame.DateTime, Is.InRange(game.DateTime.AddSeconds(-1), game.DateTime.AddSeconds(1)));



        }
         
    }
}