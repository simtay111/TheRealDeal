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

            _dataBuilder.DeleteAllData();
        }

        [Test]
        public void CanCreateNewUser()
        {
            const string username = "Bilbo";
            const string password = "Baggins";

            _userRepo.CreateUser(username, password);

            var createdUsers = DataTestHelper.GetAllAccountNodes();

            Assert.That(createdUsers.Count, Is.EqualTo(1));
            Assert.That(createdUsers[0].Password, Is.EqualTo(password));
            Assert.That(createdUsers[0].AccountName, Is.EqualTo(username));
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