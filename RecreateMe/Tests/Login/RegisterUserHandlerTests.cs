using Moq;
using NUnit.Framework;
using RecreateMe.Login;
using RecreateMe.Login.Handlers;

namespace RecreateMe.Tests.Login
{
    [TestFixture]
    public class RegisterUserHandlerTests
    {
        private string _email;
        private string _password;
        private string _confirmPassword;
        private bool _loginWasCreated;
        private string _existingUser;
        private IUserRepository _userRepo;

        [SetUp]
        public void SetUp()
        {
            _email = "Moocow@Moo.com";
            _password = "password";
            _confirmPassword = "password";
            _existingUser = "IAlreadyExist@Existing.com";
            _loginWasCreated = false;
            _userRepo = CreateMockUserRepo();
        }

        [Test]
        public void CanHandle()
        {
            _loginWasCreated = false;
            var request = CreateRequestFromFieldProperties();
            var handler = new RegisterUserHandler(_userRepo);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.True(_loginWasCreated);
        }

        [Test]
        public void ValidatesPasswordAndConfirmPasswordMatch()
        {
            _confirmPassword = "Different";
            var request = CreateRequestFromFieldProperties();
            var handler = new RegisterUserHandler(_userRepo);

            var response = handler.Handle(request);

            Assert.False(_loginWasCreated);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.PasswordsDontMatch));
        }

        [Test]
        public void ValidatesMinPasswordLength()
        {
            _password = "moo";
            _confirmPassword = "moo";
            var request = CreateRequestFromFieldProperties();
            var handler = new RegisterUserHandler(_userRepo);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.BadPasswordLength));
        }

        [Test]
        public void ValidatesMaxPasswordLength()
        {
            _password = "LopngPasswoooord";
            _confirmPassword = "LopngPasswoooord";
            var request = CreateRequestFromFieldProperties();
            var handler = new RegisterUserHandler(_userRepo);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.BadPasswordLength));
        }

        [Test]
        public void ChecksToSeeIfUserAlreadyExists()
        {
            _email = _existingUser;
            var request = CreateRequestFromFieldProperties();
            var handler = new RegisterUserHandler(_userRepo);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.UserAlreadyExists));
        }

        [Test]
        public void ValidatesLoginNameIsAnEmail()
        {
            _email = "MooMooCow";
            var request = CreateRequestFromFieldProperties();
            var handler = new RegisterUserHandler(_userRepo);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.BadUserNameFormat));
        }

        private IUserRepository CreateMockUserRepo()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(x => x.CreateUser(_email, _password)).Callback(() => _loginWasCreated = true);
            mockUserRepo.Setup(x => x.AlreadyExists(_existingUser)).Returns(true);
            return mockUserRepo.Object;
        }

        private RegisterUserRequest CreateRequestFromFieldProperties()
        {
            return new RegisterUserRequest
                       {
                           LoginEmail = _email,
                           Password = _password,
                           ConfirmPassword = _confirmPassword
                       };
        }
    }
}