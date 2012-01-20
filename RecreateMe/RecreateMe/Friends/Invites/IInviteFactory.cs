namespace RecreateMe.Friends.Invites
{
    public interface IInviteFactory
    {
        Invite CreateInvite(string eventId, string senderId, string recipientId);
    }
}