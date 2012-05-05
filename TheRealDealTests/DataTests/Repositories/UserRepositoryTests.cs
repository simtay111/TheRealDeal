using NUnit.Framework;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class UserRepositoryTests
    {
        private UserRepository _userRepo;
        readonly SampleDataBuilder _dataBuilder = new SampleDataBuilder();

        [SetUp]
        public void SetUp()
        {
            _userRepo = new UserRepository();

            SqlServerDataHelper.DeleteAllData();
        }

        [Test]
        public void CanCreateNewUser()
        {
            const string username = "Bilbo";
            const string password = "Baggins";

            _userRepo.CreateUser(username, password);
        }

        [Test]
        public void CanSeeIfUserAlreadyExists()
        {
            const string username = "Bilbo";
            const string password = "Baggins";
            _userRepo.CreateUser(username, password);

            var userExists = _userRepo.AlreadyExists(username);

            Assert.True(userExists);
        }

        [Test]
        public void DoesNotThrowIfAccountDoesNotExist()
        {
            const string username = "Bilbo";

            var userExists = _userRepo.AlreadyExists(username);

            Assert.False(userExists);
        }

        [Test]
        public void CanFindUserByNameAndPassword()
        {
            const string username = "Bilbo";
            const string password = "Baggins";
            _userRepo.CreateUser(username, password);

            var userExists = _userRepo.FoundUserByNameAndPassword(username, password);

            Assert.True(userExists);
        }

        [Test]
        public void FindingUsersByNameAndPasswordIsCaseSensitive()
        {
            const string username = "Bilbo";
            const string password = "Baggins";
            _userRepo.CreateUser(username, password);

            var userExists = _userRepo.FoundUserByNameAndPassword(username, "baggins");

            Assert.IsFalse(userExists);
        }
    }
}