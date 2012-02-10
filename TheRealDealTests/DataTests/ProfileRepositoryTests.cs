using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMeSql;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests
{
    [TestFixture]
    public class ProfileRepositoryTests
    {
        private readonly SampleDataBuilder _data = new SampleDataBuilder();
        private ProfileRepository _repo;

        [SetUp]
        public void SetUp()
        {
            _data.DeleteAllData();
            _data.CreateData();
            _repo = new ProfileRepository();
        }

        [Test]
        public void CanGetByAccount()
        {
            const string accountName = "Simtay111@gmail.com";
            var profiles = _repo.GetByAccount(accountName);

            Assert.That(profiles.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanSaveProfiles()
        {
            var profile = new Profile()
                              {
                                  AccountId = "Simtay111@gmail.com",
                                  ProfileId = "MyProfile"
                              };

            var wasSuccessful = _repo.Save(profile);

            var profiles = _repo.GetByAccount(profile.AccountId);

            Assert.True(wasSuccessful);
            Assert.That(profiles[0].ProfileId, Is.EqualTo(profile.ProfileId));
        }
    }
}