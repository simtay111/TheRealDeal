using System;
using Moq;
using NUnit.Framework;

using RecreateMe.Friends.Invites;

namespace TheRealDealTests.DomainTests.Friends.Invites
{
    [TestFixture]
    public class InviteSenderTests
    {
        private const string SenderId = "senderId";
        private const string EventId = "gameId";
        private InviteSender _inviteSender;
        private const string FriendId = "recipeintId";
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
             _inviteSender.SetEventIdForInvites(EventId);

             Assert.That(_inviteSender.EventId, Is.EqualTo(EventId));
         }

        [Test]
        public void CanSetSenderId()
        {
            CreateInviteSender();
            _inviteSender.SetSenderId(SenderId);

            Assert.That(_inviteSender.SenderId, Is.EqualTo(SenderId));
        }
        
        [Test]
        public void CanSendAnInvite()
        {
            bool inviteWasSaved = false;
            var invite = new Invite {EventId = EventId, RecepientId = FriendId, SenderId = SenderId};
            _inviteRepository.Setup(x => x.Save(invite)).Callback(() => inviteWasSaved = true);
            _inviteFactory.Setup(x => x.CreateInvite(EventId, SenderId, FriendId)).Returns(invite); 
            CreateInviteSender();
            _inviteSender.SetSenderId(SenderId);
            _inviteSender.SetEventIdForInvites(EventId);

            _inviteSender.SendInviteTo(FriendId);

            Assert.True(inviteWasSaved);
        }

        [Test]
        public void CannotSendInvitesIfSenderIsNotSpecified()
        {
            _inviteRepository.Setup(x => x.Save(It.IsAny<Invite>()));
            _inviteFactory.Setup(x => x.CreateInvite(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new Invite());
            CreateInviteSender();
            _inviteSender.SetEventIdForInvites(EventId);

            var exception = Assert.Throws(typeof(Exception), () => _inviteSender.SendInviteTo(FriendId));
            Assert.That(exception.Message, Is.EqualTo("Both Sender Id and Event Id is required to send invites"));
        }

        [Test]
        public void CannotSendInvitesIfEventIdIsNotSpecified()
        {
            _inviteRepository.Setup(x => x.Save(It.IsAny<Invite>()));
            _inviteFactory.Setup(x => x.CreateInvite(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new Invite());
            CreateInviteSender();
            _inviteSender.SetSenderId(SenderId);

            var exception = Assert.Throws(typeof(Exception), () => _inviteSender.SendInviteTo(FriendId));
            Assert.That(exception.Message, Is.EqualTo("Both Sender Id and Event Id is required to send invites"));
        }

        public void CreateInviteSender()
        {
            _inviteSender = new InviteSender(_inviteRepository.Object, _inviteFactory.Object);
        }
    }
}