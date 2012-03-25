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
    public class TeamGameRepositoryTests
    {
        private TeamGameRepository _repo;
        private SampleDataBuilder _data;

        [SetUp]
        public void SetUp()
        {
            _repo = new TeamGameRepository();
            _data = new SampleDataBuilder();
            _data.DeleteAllData();
        }

        [Test]
        public void CanSaveAndGetGamesWithTeams()
        {
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();
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
            game.Creator = profile.ProfileId;

            _repo.SaveTeamGame(game);

            var retrievedGame = _repo.GetTeamGameById(game.Id);

            Assert.That(game.Id, Is.EqualTo(retrievedGame.Id));
            Assert.That(retrievedGame.Location.Name, Is.EqualTo(game.Location.Name));
            Assert.That(retrievedGame.Sport.Name, Is.EqualTo(game.Sport.Name));
            Assert.That(retrievedGame.IsPrivate, Is.EqualTo(game.IsPrivate));
            Assert.That(retrievedGame.MinPlayers, Is.EqualTo(game.MinPlayers));
            Assert.That(retrievedGame.MaxPlayers, Is.EqualTo(game.MaxPlayers));
            Assert.That(retrievedGame.DateTime, Is.InRange(game.DateTime.AddSeconds(-1), game.DateTime.AddSeconds(1)));
            Assert.That(retrievedGame.TeamsIds[0], Is.EqualTo(team.Id));
            Assert.That(retrievedGame.Creator, Is.EqualTo(game.Creator));
        }

        [Test]
        public void CanGetListOfGamesThatProfileIsPartOfIncludingTeams()
        {
            _data.CreateData();

            var teamGames = _repo.GetTeamGamesForProfile("Simtay111");

            Assert.That(teamGames.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanAddTeamsToGame()
        {
            _data.CreateData();

            _repo.AddTeamToGame(_data.TeamId2, _data.GameWithTeamsId);

            var game = _repo.GetTeamGameById(_data.GameWithTeamsId);

            Assert.That(game.TeamsIds.Count, Is.EqualTo(3));
            Assert.True(game.TeamsIds.Single(x => x == _data.TeamId2).Any());
        }
    }
}