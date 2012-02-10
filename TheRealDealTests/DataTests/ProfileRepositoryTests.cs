using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMeSql;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests
{
    [TestFixture]
    public class ProfileRepositoryTests
    {
        private const string AccountId = "Simtay111@gmail.com";
        private readonly SampleDataBuilder _data = new SampleDataBuilder();
        private ProfileRepository _repo;
        private const string ProfileId = "MyProfile";
        private const string Profile1Name = "Simtay111";

        [SetUp]
        public void SetUp()
        {
            _data.DeleteAllData();
            _repo = new ProfileRepository();
        }

        [Test]
        public void CanGetByAccount()
        {
            _data.CreateData();

            const string accountId = AccountId;
            var profiles = _repo.GetByAccount(accountId);

            Assert.That(profiles.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanSaveProfiles()
        {
            _data.CreateData();
            var profile = new Profile()
                              {
                                  AccountId = AccountId,
                                  ProfileId = ProfileId
                              };

            var wasSuccessful = _repo.Save(profile);

            var profiles = _repo.GetByAccount(profile.AccountId);
            Assert.True(wasSuccessful);
            Assert.That(profiles[0].ProfileId, Is.EqualTo(profile.ProfileId));
        }

        [Test]
        public void CanCheckIfAProfileAlreadyExistsWithProfileName()
        {
            _data.CreateAccount1();
            _data.CreateProfileForAccount1();

            Assert.True(_repo.ProfileExistsWithName(Profile1Name));
            Assert.False(_repo.ProfileExistsWithName(ProfileId));
        }
    }
}