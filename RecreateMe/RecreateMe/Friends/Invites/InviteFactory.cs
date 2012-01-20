namespace RecreateMe.Friends.Invites
{
    public class InviteFactory : IInviteFactory
    {
        public Invite CreateInvite(string eventId, string senderId, string recipientId)
        {
            return new Invite {EventId = eventId, SenderId = senderId, RecepientId = recipientId};
        }
    }
}