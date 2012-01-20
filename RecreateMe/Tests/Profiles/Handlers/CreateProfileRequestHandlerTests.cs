using Moq;
using NUnit.Framework;
using RecreateMe.Exceptions;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;
using RecreateMe.Sports;

namespace RecreateMe.Tests.Profiles.Handlers
{
    [TestFixture]
    public class CreateProfileRequestHandlerTests
    {
        private Mock<ILocationRepository> _mockLocationRepo;
        private Mock<ISportRepository> _mockSportRepo;
        private Mock<IProfileRepository> _mockProfileUpdater;
        private Mock<ProfileBuilder> _mockProfileBuilder;
        private Profile _profile;

        [SetUp]
        public void SetUp()
        {
            _profile = new Profile();
        }

        [Test]
        public void ResponseContainsAPerson()
        {
            CreateMockRepositories();
            var request = new CreateProfileRequest("UserId", "Simon Taylor", "location", "Soccer", "10");
            var handler = CreateProfileRequestHandler();
            var response = handler.Handle(request);
            Assert.NotNull(response);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void CanBeHandledWithOnlyName()
        {
            CreateMockRepositories();
            var request = new CreateProfileRequest("UserId", "Bob");
            var handler = CreateProfileRequestHandler();
            var response = handler.Handle(request);
            Assert.NotNull(response);
        }

        [Test]
        public void CreatingNameWithEmptyStringThrowsNotEnoughInfoException()
        {
            CreateMockRepositories();
            var request = new CreateProfileRequest("UserId", "");
            var handler = CreateProfileRequestHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NameNotSpecified));
        }

        [Test]
        public void AddsProfileToCurrentUserLogin()
        {
            CreateMockRepositories();

            var request = new CreateProfileRequest("UserId", "Happy Days");
            var handler = CreateProfileRequestHandler();

            handler.Handle(request);

            Assert.That(_profile.UserId, Is.EqualTo("UserId"));
        }

        private CreateProfileRequestHandler CreateProfileRequestHandler()
        {
            var handler = new CreateProfileRequestHandler(_mockSportRepo.Object, _mockLocationRepo.Object,
                                                          _mockProfileUpdater.Object, _mockProfileBuilder.Object);
            return handler;
        }

        private void CreateMockRepositories()
        {
            _mockLocationRepo = new Mock<ILocationRepository>();
            _mockLocationRepo.Setup(x => x.FindByName(It.IsAny<string>())).Returns(new Location(5));
            _mockSportRepo = new Mock<ISportRepository>();
            _mockSportRepo.Setup(x => x.FindByName(It.IsAny<string>())).Returns(new Sport());
            _mockProfileUpdater = new Mock<IProfileRepository>();
            _mockProfileUpdater.Setup(x => x.SaveOrUpdate(It.IsAny<Profile>())).Returns(true);
            _mockProfileBuilder = new Mock<ProfileBuilder>() {CallBase = true};
            _mockProfileBuilder.Setup(x => x.Build()).Returns(_profile);
        }
    }
}
