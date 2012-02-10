using Moq;
using NUnit.Framework;
using RecreateMe.Exceptions;
using RecreateMe.Friends.Invites;

namespace TheRealDealTests.DomainTests.Friends.Invites
{
    [TestFixture]
    public class InviteSenderTests
    {
        private string _senderId = "senderId";
        private string _eventId = "gameId";
        private InviteSender _inviteSender;
        private string _friendId = "recipeintId";
        private Mock<IInviteRepository> _inviteRepository;
        private Mock<IInviteFactory> _inviteFactory;

        [SetUp]
        public void SetUp()
        {
            _inviteFactory = new Mock<IInviteFactory>();   
            _inviteRepository = new Mock<IInviteRepository>();
        }

        [Test]
        public void CanSetGameId()
         {
            CreateInviteSender();
             _inviteSender.SetEventIdForInvites(_eventId);

             Assert.That((object) _inviteSender.EventId, Is.EqualTo(_eventId));
         }

        [Test]
        public void CanSetSenderId()
        {
            CreateInviteSender();
            _inviteSender.SetSenderId(_senderId);

            Assert.That((object) _inviteSender.SenderId, Is.EqualTo(_senderId));
        }
        
        [Test]
        public void CanSendAnInvite()
        {
            bool inviteWasSaved = false;
            var invite = new Invite {EventId = _eventId, RecepientId = _friendId, SenderId = _senderId};
            _inviteRepository.Setup(x => x.Save(invite)).Callback(() => inviteWasSaved = true);
            _inviteFactory.Setup(x => x.CreateInvite(_eventId, _senderId, _friendId)).Returns(invite); 
            CreateInviteSender();
            _inviteSender.SetSenderId(_senderId);
            _inviteSender.SetEventIdForInvites(_eventId);

            _inviteSender.SendInviteTo(_friendId);

            Assert.True(inviteWasSaved);
        }

        [Test]
        public void CannotSendInvitesIfSenderIsNotSpecified()
        {
            _inviteRepository.Setup(x => x.Save(It.IsAny<Invite>()));
            _inviteFactory.Setup(x => x.CreateInvite(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new Invite());
            CreateInviteSender();
            _inviteSender.SetEventIdForInvites(_eventId);

            var exception = Assert.Throws(typeof(NotEnoughInfoException), () => _inviteSender.SendInviteTo(_friendId));
            Assert.That(exception.Message, Is.EqualTo("Both Sender Id and Event Id is required to send invites"));
        }

        [Test]
        public void CannotSendInvitesIfEventIdIsNotSpecified()
        {
            _inviteRepository.Setup(x => x.Save(It.IsAny<Invite>()));
            _inviteFactory.Setup(x => x.CreateInvite(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new Invite());
            CreateInviteSender();
            _inviteSender.SetSenderId(_senderId);

            var exception = Assert.Throws(typeof(NotEnoughInfoException), () => _inviteSender.SendInviteTo(_friendId));
            Assert.That(exception.Message, Is.EqualTo("Both Sender Id and Event Id is required to send invites"));
        }

        public void CreateInviteSender()
        {
            _inviteSender = new InviteSender(_inviteRepository.Object, _inviteFactory.Object);
        }
    }
}