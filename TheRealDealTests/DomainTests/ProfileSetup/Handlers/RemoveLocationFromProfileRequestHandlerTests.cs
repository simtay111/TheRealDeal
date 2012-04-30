using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.ProfileSetup.Handlers;
using RecreateMe.Profiles;

namespace TheRealDealTests.DomainTests.ProfileSetup.Handlers
{
    [TestFixture]
    public class RemoveLocationFromProfileRequestHandlerTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void CanRemoveLocationFromProfile()
        {
            var request = new RemoveLocationFromProfileRequest {ProfileId = "ProfileId", LocationName = "Bend"};
            var profileRepo = new Mock<IProfileRepository>();
            profileRepo.Setup(x => x.RemoveLocationFromProfile(request.ProfileId, request.LocationName));

            var handler = new RemoveLocationFromProfileRequestHandler(profileRepo.Object);

            var response = handler.Handle(request);

            profileRepo.Verify(x => x.RemoveLocationFromProfile(request.ProfileId, request.LocationName));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }
    }
}
