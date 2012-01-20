using RecreateMe.Exceptions;

namespace RecreateMe.Friends.Invites
{
    public class InviteSender : IInviteSender
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IInviteFactory _inviteFactory;

        public InviteSender(IInviteRepository inviteRepository, IInviteFactory inviteFactory)
        {
            _inviteRepository = inviteRepository;
            _inviteFactory = inviteFactory;
        }

        public string EventId { get; set; }
        public string SenderId { get; set; }

        public void SendInviteTo(string friendId)
        {
            if (SenderId == null || EventId == null)
                throw new NotEnoughInfoException("Both Sender Id and Event Id is required to send invites");

            var invite = _inviteFactory.CreateInvite(EventId, SenderId, friendId);

            _inviteRepository.Save(invite);
        }

        public void SetEventIdForInvites(string gameId)
        {
            EventId = gameId;
        }

        public void SetSenderId(string id)
        {
            SenderId = id;
        }
    }
}