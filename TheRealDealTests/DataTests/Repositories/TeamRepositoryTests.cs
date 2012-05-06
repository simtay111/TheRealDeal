using System.Collections.Generic;
using NUnit.Framework;
using RecreateMe.Teams;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    [Ignore]
    public class TeamRepositoryTests
    {
        private TeamRepository _repo;
        private SampleDataBuilder _data;

        [SetUp] 
        public void SetUp()
        {
            _repo = new TeamRepository();
            _data = new SampleDataBuilder();
        }

        [Test]
        public void CanGetById()
        {
            _data.CreateAccountWithProfile1();
            _data.CreateAccountWithProfile2();
            var team = _data.CreateTeam2();

            var retrievedTeam = _repo.GetById(team.Id);
            Assert.That(retrievedTeam.MaxSize, Is.EqualTo(team.MaxSize));
            Assert.That(retrievedTeam.Name, Is.EqualTo(team.Name));
        }

        [Test]
        public void CanSave()
        {
            _data.CreateAccount1();
            var profile1 = _data.CreateProfileForAccount1();
            _data.CreateAccount2();
            var profile2 = _data.CreateProfileForAccount2();


            var team = new Team
                           {
                               MaxSize = 14,
                               Name = "MyBestTeam",
                               PlayersIds = new List<string> {profile1.ProfileId, profile2.ProfileId},
                               Creator = profile1.ProfileId
                           };

            var saved = _repo.Save(team);

            var retrievedTeam = _repo.GetById(team.Id);
            Assert.True(saved);
            Assert.That(retrievedTeam.MaxSize, Is.EqualTo(team.MaxSize));
            Assert.That(retrievedTeam.Name, Is.EqualTo(team.Name));
            Assert.That(retrievedTeam.PlayersIds.Count, Is.EqualTo(2));
            Assert.That(retrievedTeam.PlayersIds[0], Is.EqualTo(profile1.ProfileId));
            Assert.That(retrievedTeam.PlayersIds[1], Is.EqualTo(profile2.ProfileId));
            Assert.That(retrievedTeam.Creator, Is.EqualTo(profile1.ProfileId));
        }

        [Test]
        public void CanGetListOfTeamsForProfile()
        {
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();
            var team = new Team
            {
                MaxSize = 14,
                Name = "MyBestTeam",
                PlayersIds = new List<string> { profile.ProfileId },
                Creator = profile.ProfileId
            };
            _repo.Save(team);

            var teams = _repo.GetTeamsForProfile(profile.ProfileId);

            Assert.That(teams.Count, Is.EqualTo(1));
            Assert.That(teams[0].Name, Is.EqualTo(team.Name));
            Assert.That(teams[0].MaxSize, Is.EqualTo(team.MaxSize));
        }
    }
}