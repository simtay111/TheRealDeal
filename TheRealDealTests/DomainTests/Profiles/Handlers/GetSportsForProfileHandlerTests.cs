using Moq;
using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;

namespace TheRealDealTests.DomainTests.Profiles.Handlers
{
    [TestFixture]
    public class GetSportsForProfileHandlerTests
    {
        [Test]
        public void CanGetSportsForAGivenProfile()
        {
            const string profileId = "profile1";
            var profile = TestData.MockProfile1();
            var profileRepo = new Mock<IProfileRepository>();
            profileRepo.Setup(x => x.GetByProfileId(profileId)).Returns(profile);
            var request = new GetSportsForProfileRequest { ProfileId = profileId};
            var handler = new GetSportsForProfileHandler(profileRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.SportsForProfile.Count, Is.EqualTo(1));
            Assert.That(response.SportsForProfile[0], Is.EqualTo(profile.SportsPlayed[0]));
        }
    }
}