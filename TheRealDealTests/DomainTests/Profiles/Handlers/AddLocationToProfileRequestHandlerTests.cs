using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;

namespace TheRealDealTests.DomainTests.Profiles.Handlers
{
    [TestFixture]
    public class AddLocationToProfileRequestHandlerTests
    {
        private Mock<ILocationRepository> _mockLocationRepo;
        private Mock<IProfileRepository> _mockProfileRepo;
        private Profile _profile;
        private bool _profileWasSavedSuccessfully;

        [SetUp]
        public void SetUp()
        {
            _profileWasSavedSuccessfully = false;
        }

        [Test]
        public void CanAddLocationToProfile()
        {
            const string location = "Bend";
            const string profileId = "123";
            _profile = new Profile
                           {
                               ProfileName = profileId
                           };
            var request = new AddLocationToProfileRequest
                              {
                                  Location = location,
                                  ProfileId = profileId
                              };
            CreateMockProfileAndLocationRepos(profileId, location);
            var handler = new AddLocationToProfileRequestHandle(_mockProfileRepo.Object, _mockLocationRepo.Object);

            var response = handler.Handle(request);

            Assert.That(_profile.Locations.Count, Is.Not.EqualTo(0));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.True(_profileWasSavedSuccessfully);
        }

        [Test]
        public void ThrowsExceptionIfLocationWasNotSpeicified()
        {
            var request = new AddLocationToProfileRequest
                              {
                                  Location = null
                              };
            CreateMockProfileAndLocationRepos("Moo", null);
            var handler = new AddLocationToProfileRequestHandle(_mockProfileRepo.Object, _mockLocationRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.LocationNotSpecified));
            Assert.False(_profileWasSavedSuccessfully);
        }

        [Test]
        public void ReturnsLocationNotFoundStatusIfTheLocationSpecifiedCouldNotBeFound()
        {
            var request = new AddLocationToProfileRequest
                              {
                Location = "Hamsterton"
            };
            CreateMockProfileAndLocationRepos("Moo", request.Location);
            _mockLocationRepo.Setup(x => x.FindByName(request.Location)).Returns(() => null);
            var handler = new AddLocationToProfileRequestHandle(_mockProfileRepo.Object, _mockLocationRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.LocationNotFound));
            Assert.False(_profileWasSavedSuccessfully);
        }

        [Test]
        public void ReturnsLocationAlreadyInProfileIfTheLocationSpecifiedIsAlreadyAttachedToThePRofile()
        {
            const string profileId = "profId";
            var request = new AddLocationToProfileRequest
                              {
                                  Location = "Bend",
                                  ProfileId = profileId
                              };
            _profile = new Profile();
            _profile.Locations.Add(new Location("Bend"));
            CreateMockProfileAndLocationRepos(profileId, request.Location);
            var handler = new AddLocationToProfileRequestHandle(_mockProfileRepo.Object, _mockLocationRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.LocationAlreadyInProfile));
            Assert.That(_profile.Locations.Count, Is.EqualTo(1));
        }

        private void CreateMockProfileAndLocationRepos(string profileId, string location)
        {
            _mockLocationRepo = new Mock<ILocationRepository>();
            _mockLocationRepo.Setup(x => x.FindByName(location)).Returns(new Location("Bend"));
            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.GetByProfileId(profileId)).Returns(_profile);
            _mockProfileRepo.Setup(x => x.AddLocationToProfile(It.IsAny<Profile>(), It.IsAny<Location>())).Callback(() => _profileWasSavedSuccessfully = true);
        }
    }
}