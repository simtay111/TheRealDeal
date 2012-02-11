using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Profiles.Handlers
{
    [TestFixture]
    public class CreateProfileRequestHandlerTests
    {
        private Mock<ILocationRepository> _mockLocationRepo;
        private Mock<ISportRepository> _mockSportRepo;
        private Mock<IProfileRepository> _mockProfileRepo;
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
            var request = new CreateProfileRequest("AccountId", "Simon Taylor", "location", "Soccer", "10");
            var handler = CreateProfileRequestHandler();
            var response = handler.Handle(request);
            Assert.NotNull(response);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void CanBeHandledWithOnlyName()
        {
            CreateMockRepositories();
            var request = new CreateProfileRequest("AccountId", "Bob");
            var handler = CreateProfileRequestHandler();
            var response = handler.Handle(request);
            Assert.NotNull(response);
        }

        [Test]
        public void CreatingNameWithEmptyStringThrowsNotEnoughInfoException()
        {
            CreateMockRepositories();
            var request = new CreateProfileRequest("AccountId", "");
            var handler = CreateProfileRequestHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NameNotSpecified));
        }

        [Test]
        public void AddsProfileToCurrentUserLogin()
        {
            CreateMockRepositories();

            var request = new CreateProfileRequest("AccountId", "Happy Days");
            var handler = CreateProfileRequestHandler();

            handler.Handle(request);

            Assert.That(_profile.AccountId, Is.EqualTo("AccountId"));
        }

        [Test]
        public void ReturnsIfAccountAlreadyHasMaxProfiles()
        {
            CreateMockRepositories();
            const string accountName = "NoProfiles";
            _mockProfileRepo.Setup(x => x.GetByAccount(accountName))
                .Returns(new List<Profile> { new Profile(), new Profile(), new Profile()});

            var request = new CreateProfileRequest(accountName, "MyAccount");
            var handler = CreateProfileRequestHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.MaxProfilesReached));
        }

        [Test]
        public void ChecksToSeeIfProfileNameIsAlreadyInUse()
        {
            CreateMockRepositories();
            const string accountName = "NoProfiles";

            var request = new CreateProfileRequest(accountName, "MyAccount");
            _mockProfileRepo.Setup(x => x.GetByAccount(accountName)).Returns(new List<Profile>());
            _mockProfileRepo.Setup(x => x.ProfileExistsWithName(request.ProfileId)).Returns(true);
            var handler = CreateProfileRequestHandler();

           var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.ProfileNameAlreadyExists));
        }

        private CreateProfileRequestHandler CreateProfileRequestHandler()
        {
            var handler = new CreateProfileRequestHandler(_mockSportRepo.Object, _mockLocationRepo.Object,
                                                          _mockProfileRepo.Object, _mockProfileBuilder.Object);
            return handler;
        }

        private void CreateMockRepositories()
        {
            _mockLocationRepo = new Mock<ILocationRepository>();
            _mockLocationRepo.Setup(x => x.FindByName(It.IsAny<string>())).Returns(new Location("Bend"));
            _mockSportRepo = new Mock<ISportRepository>();
            _mockSportRepo.Setup(x => x.FindByName(It.IsAny<string>())).Returns(new Sport());
            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.Save(It.IsAny<Profile>())).Returns(true);
            _mockProfileRepo.Setup(x => x.GetByAccount(It.IsAny<string>())).Returns(new List<Profile> { new Profile()});
            _mockProfileBuilder = new Mock<ProfileBuilder> {CallBase = true};
            _mockProfileBuilder.Setup(x => x.Build()).Returns(_profile);
        }
    }
}
