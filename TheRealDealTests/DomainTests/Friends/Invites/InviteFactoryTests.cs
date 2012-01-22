using NUnit.Framework;
using RecreateMe.Friends.Invites;

namespace TheRealDealTests.DomainTests.Friends.Invites
{
    [TestFixture]
    public class InviteFactoryTests
    {
        private const string EventId = "eventId";
        private const string RepicientId = "recipient";
        private const string SenderId = "senderId";

        [Test]
        public void CanCreateInvite()
         {
             var factory = new InviteFactory();

             var invite = factory.CreateInvite(EventId, SenderId, RepicientId);

             Assert.NotNull(invite); 
             Assert.AreEqual(invite.EventId, EventId);
             Assert.AreEqual(invite.SenderId, SenderId);
             Assert.AreEqual(invite.RecepientId, RepicientId);
         }
    }
}