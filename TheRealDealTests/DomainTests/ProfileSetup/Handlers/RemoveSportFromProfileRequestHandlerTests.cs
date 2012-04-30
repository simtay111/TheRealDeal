using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.ProfileSetup.Handlers;
using RecreateMe.Profiles;

namespace TheRealDealTests.DomainTests.ProfileSetup.Handlers
{
    [TestFixture]
    public class RemoveSportFromProfileRequestHandlerTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void CanRemoveSPortsFromProfile()
        {
            var request = new RemoveSportFromProfileRequest {ProfileId = "ProfileId", SportName = "Soccer"};
            var profileRepo = new Mock<IProfileRepository>();
            profileRepo.Setup(x => x.RemoveSportFromProfile(request.ProfileId, request.SportName));

            var handler = new RemoveSportFromProfileRequestHandler(profileRepo.Object);

            var response = handler.Handle(request);

            profileRepo.Verify(x => x.RemoveSportFromProfile(request.ProfileId, request.SportName));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }
    }
}
