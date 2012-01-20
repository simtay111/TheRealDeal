namespace RecreateMe.Friends.Invites
{
    public interface IInviteSender
    {
        void SendInviteTo(string friendId);
        void SetEventIdForInvites(string gameId);
        void SetSenderId(string inviterId);
    }
}