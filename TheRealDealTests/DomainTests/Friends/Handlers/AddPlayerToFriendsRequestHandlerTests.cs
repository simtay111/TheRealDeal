using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Friends.Handlers;
using RecreateMe.Profiles;

namespace TheRealDealTests.DomainTests.Friends.Handlers
{
    [TestFixture]
    public class AddPlayerToFriendsRequestHandlerTests
    {
        private Mock<IProfileRepository> _mockProfileRepo;

        [Test] 
        public void CanAddPlayerToFriends()
        {
            bool profileWasSaved = false;
            var request = new AddPlayerToFriendsRequest() {FriendId = "SomeId", ProfileId = "ProfId"};

            var profile = new Profile();

            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.GetByProfileId(request.ProfileId)).Returns(profile);
            _mockProfileRepo.Setup(x => x.SaveOrUpdate(profile)).Callback(() => profileWasSaved = true);

            var handler = new AddPlayerToFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.True(profileWasSaved);
            Assert.That((object) profile.FriendsIds[0], Is.EqualTo(request.FriendId));
            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.Success));
        }
    }
}