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

        [Test]
        public void CanAddLocationToProfile()
        {
            const string location = "Bend";
            const string profileId = "123";
            _profile = new Profile
                           {
                                  UniqueId = profileId
                              };
            var request = new AddLocationToProfileRequest
                              {
                                  Location = location,
                                  ProfileId = profileId
                              };
            CreateMockProfileAndLocationRepos(profileId, location);
            var handler = new AddLocationToProfileRequestHandler(_mockProfileRepo.Object, _mockLocationRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) _profile.Locations.Count, Is.Not.EqualTo(0));
            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void ThrowsExceptionIfLocationWasNotSpeicified()
        {
            var request = new AddLocationToProfileRequest()
                              {
                                  Location = null
                              };
            CreateMockProfileAndLocationRepos("Moo", null);
            var handler = new AddLocationToProfileRequestHandler(_mockProfileRepo.Object, _mockLocationRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.LocationNotSpecified));
        }

        private void CreateMockProfileAndLocationRepos(string profileId, string location)
        {
            _mockLocationRepo = new Mock<ILocationRepository>();
            _mockLocationRepo.Setup(x => x.FindByName(It.Is<string>(d => d == location))).Returns(new Location(1));
            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.GetByUniqueId(It.Is<string>(d => d == profileId))).Returns(_profile);
            _mockProfileRepo.Setup(x => x.SaveOrUpdate(It.IsAny<Profile>())).Returns(true);
        }
    }
}