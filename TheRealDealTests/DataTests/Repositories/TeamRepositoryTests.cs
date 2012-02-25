using NUnit.Framework;
using RecreateMe.Teams;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class TeamRepositoryTests
    {
        private TeamRepository _repo;
        private SampleDataBuilder _data;

        [SetUp] 
        public void SetUp()
        {
            _repo = new TeamRepository();
            _data = new SampleDataBuilder();
            _data.DeleteAllData();
        }

        [Test]
        public void CanGetById()
        {
            var team = _data.CreateTeam1();

            var retrievedTeam = _repo.GetById(team.Id);
            Assert.That(retrievedTeam.MaxSize, Is.EqualTo(team.MaxSize));
            Assert.That(retrievedTeam.Name, Is.EqualTo(team.Name));
        }

        [Test]
        public void CanSave()
        {
            var team = new Team
                           {
                               MaxSize = 14,
                               Name = "MyBestTeam",
                           };

            var saved = _repo.Save(team);


            var retrievedTeam = _repo.GetById(team.Id);
            Assert.True(saved);
            Assert.That(retrievedTeam.MaxSize, Is.EqualTo(team.MaxSize));
            Assert.That(retrievedTeam.Name, Is.EqualTo(team.Name));
        }
    }
}