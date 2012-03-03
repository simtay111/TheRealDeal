using System;
using System.Linq;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class GameRepositoryTests
    {
        private GameRepository _repo;
        private SampleDataBuilder _data;

        [SetUp]
        public void SetUp()
        {
            _repo = new GameRepository();
            _data = new SampleDataBuilder();
            _data.DeleteAllData();
        }

        [Test]
        public void CanSaveAndGetGamesWithoutTeams()
        {
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
            game.AddPlayer(profile.ProfileId);

            _repo.Save(game);

            var retrievedGame = _repo.GetById(game.Id) as GameWithoutTeams;

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
            Assert.That(retrievedGame.Location.Name, Is.EqualTo(game.Location.Name));
            Assert.That(retrievedGame.Sport.Name, Is.EqualTo(game.Sport.Name));
            Assert.That(retrievedGame.IsPrivate, Is.EqualTo(game.IsPrivate));
            Assert.That(retrievedGame.MinPlayers, Is.EqualTo(game.MinPlayers));
            Assert.That(retrievedGame.MaxPlayers, Is.EqualTo(game.MaxPlayers));
            Assert.That(retrievedGame.DateTime, Is.InRange(game.DateTime.AddSeconds(-1), game.DateTime.AddSeconds(1)));
            Assert.That(retrievedGame.PlayersIds[0], Is.EqualTo(profile.ProfileId));
        }

        [Test]
        public void CanSaveAndGetGamesWithTeams()
        {
            _data.CreateLocationBend();
            _data.CreateSoccerSport();
            var team = _data.CreateTeam1();

            var game = new GameWithTeams(DateTimeOffset.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = new Sport("Soccer");
            game.Location = new Location("Bend");
            game.AddTeam(team.Id);

            _repo.Save(game);

            var retrievedGame = _repo.GetById(game.Id) as GameWithTeams;

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
            Assert.That(retrievedGame.Location.Name, Is.EqualTo(game.Location.Name));
            Assert.That(retrievedGame.Sport.Name, Is.EqualTo(game.Sport.Name));
            Assert.That(retrievedGame.IsPrivate, Is.EqualTo(game.IsPrivate));
            Assert.That(retrievedGame.MinPlayers, Is.EqualTo(game.MinPlayers));
            Assert.That(retrievedGame.MaxPlayers, Is.EqualTo(game.MaxPlayers));
            Assert.That(retrievedGame.DateTime, Is.InRange(game.DateTime.AddSeconds(-1), game.DateTime.AddSeconds(1)));
            Assert.That(retrievedGame.TeamsIds[0], Is.EqualTo(team.Id));
        }

        [Test]
        public void CanGetListOfGamesThatProfileIsPartOfIncludingTeams()
        {
            _data.CreateData();

            var games = _repo.GetForProfile("Simtay111");

            Assert.That(games.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanGetByLocation()
        {
            _data.CreateData();

            var games = _repo.FindByLocation("Bend");

            Assert.That(games.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanAddProfilesToGame()
        {
            _data.CreateData();

            const string profileId = "Profile1";
            _repo.AddPlayerToGame(_data.GameWithoutTeams.Id, profileId);

            var game = _repo.GetById(_data.GameWithoutTeams.Id) as GameWithoutTeams;

            Assert.That(game.PlayersIds.Any(x => x == profileId));
        }

        [Test]
        public void CanAddTeamsToGame()
        {
            _data.CreateData();

           _repo.AddTeamToGame(_data.TeamId2, _data.GameWithTeamsId);

            var game = _repo.GetById(_data.GameWithTeamsId) as GameWithTeams;

            Assert.That(game.TeamsIds.Count, Is.EqualTo(2));
            Assert.True(game.TeamsIds.Single(x => x == _data.TeamId2).Any());
        }

    }
}