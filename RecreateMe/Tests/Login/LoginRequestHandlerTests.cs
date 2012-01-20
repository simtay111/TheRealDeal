﻿using Moq;
using NUnit.Framework;
using RecreateMe.Login;
using RecreateMe.Login.Handlers;

namespace RecreateMe.Tests.Login
{
    [TestFixture]
    public class LoginRequestHandlerTests
    {
        private const string Username = "Simtay111";
        private const string Password = "Password";
        private Mock<IUserRepository> _mockUserRepo;

        [Test]
        public void CanLoginSuccessfullyWithCorrcectUserNameAndPassword()
        {
            var request = new LoginRequest {Username = Username, Password = Password};
            SetupMockUserRepo();
            var handler = new LoginRequestHandler(_mockUserRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void ReturnsFailureCodeIfAUserCouldNotBeFoundWithCredentials()
        {
            var request = new LoginRequest { Username = "Hamster", Password = Password };
            SetupMockUserRepo();
            var handler = new LoginRequestHandler(_mockUserRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Failure));
        }

        private void SetupMockUserRepo()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockUserRepo.Setup(x => x.FoundUserByNameAndPassword(Username, Password)).Returns(true);
        }
    }
}