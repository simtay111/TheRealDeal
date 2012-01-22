using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Friends.Invites;
using RecreateMe.Friends.Invites.Handlers;

namespace TheRealDealTests.DomainTests.Friends.Invites.Handlers
{
    [TestFixture]
    public class InviteFriendsToGameRequestHandlerTests
    {
        private Mock<IInviteSender> _mockInviteSender;

        [Test]
        public void CanInviteFriends()
         {
             var request = new InviteFriendsToGameRequest();
             request.FriendIds.Add("123");
             request.GameId = "gamedId";
             request.InviterId = "Simtay111";
             _mockInviteSender = new Mock<IInviteSender>();
             _mockInviteSender.Setup(x => x.SetEventIdForInvites(request.GameId)).Verifiable();
             _mockInviteSender.Setup(x => x.SetSenderId(request.InviterId)).Verifiable();
             _mockInviteSender.Setup(x => x.SendInviteTo(request.FriendIds[0])).Verifiable();
             var handler = new InviteFriendsToGameRequestHandler(_mockInviteSender.Object);

             var response = handler.Handle(request);

            _mockInviteSender.VerifyAll();
             Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.Success));
         }
    }
}