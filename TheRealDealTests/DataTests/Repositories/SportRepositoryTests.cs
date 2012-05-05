using NUnit.Framework;
using RecreateMeSql.Repositories;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class SportRepositoryTests
    {
        private SportRepository _repo;

        [SetUp]
        public void SetUp()
        {
            SqlServerDataHelper.DeleteAllData();
            _repo = new SportRepository();
        }

        [Test]
        public void CanFindSportsByName()
        {
            _repo.CreateSport("Soccer");
            const string soccerName = "Soccer";

            var sport = _repo.FindByName(soccerName);

            Assert.NotNull(sport);
            Assert.That(sport.Name, Is.EqualTo(soccerName));
        }

        [Test]
        public void CanCreateSports()
        {
            _repo.CreateSport("Soccer");

            var sport = _repo.FindByName("Soccer");
            Assert.That(sport, Is.Not.Null);
        }

        [Test]
        public void CanGetListOfAllSports()
        {
            _repo.CreateSport("Soccer");
            _repo.CreateSport("Basketball");
            _repo.CreateSport("Football");

            var sports = _repo.GetNamesOfAllSports();

            Assert.That(sports[0], Is.EqualTo("Basketball"));
            Assert.That(sports[1], Is.EqualTo("Football"));
            Assert.That(sports[2], Is.EqualTo("Soccer"));
        }
    }
}