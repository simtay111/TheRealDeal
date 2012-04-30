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
            var friendProfile = new Profile();

            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.GetByProfileId(request.ProfileId)).Returns(profile);
            _mockProfileRepo.Setup(x => x.GetByProfileId(request.FriendId)).Returns(friendProfile);
            _mockProfileRepo.Setup(x => x.AddFriendToProfile(profile.ProfileId, friendProfile.ProfileId)).Callback(() => profileWasSaved = true);

            var handler = new AddPlayerToFriendsRequestHandle(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.True(profileWasSaved);
            Assert.That(profile.FriendsIds[0], Is.SameAs(friendProfile.ProfileId));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void WontAddPlayerIfTheyAreAlreadyAFriend()
        {
            var request = new AddPlayerToFriendsRequest() { FriendId = "SomeId", ProfileId = "ProfId" };

            var profile = new Profile();
            var friendProfile = new Profile() {ProfileId = request.FriendId};
            profile.FriendsIds.Add(friendProfile.ProfileId);

            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.GetByProfileId(request.ProfileId)).Returns(profile);
            //_mockProfileRepo.Setup(x => x.GetByProfileId(request.FriendId)).Returns(friendProfile);
            //_mockProfileRepo.Setup(x => x.AddFriendToProfile(profile.ProfileId, friendProfile.ProfileId)).Callback(() => profileWasSaved = true);

            var handler = new AddPlayerToFriendsRequestHandle(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.AlreadyFriend));
        }
    }
}