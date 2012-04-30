using System.Collections.Generic;

namespace RecreateMe.Friends.Invites.Handlers
{
    public class InviteFriendsToGameRequestHandle : IHandle<InviteFriendsToGameRequest, InviteFriendsToGameResponse>
    {
        private readonly IInviteSender _inviteSender;

        public InviteFriendsToGameRequestHandle(IInviteSender inviteSender)
        {
            _inviteSender = inviteSender;
        }

        public InviteFriendsToGameResponse Handle(InviteFriendsToGameRequest request)
        {
            _inviteSender.SetEventIdForInvites(request.GameId);
            _inviteSender.SetSenderId(request.InviterId);

            foreach (var friendId in request.FriendIds)
            {
                _inviteSender.SendInviteTo(friendId);
            }

            return new InviteFriendsToGameResponse(ResponseCodes.Success);
        }
    }

    public class InviteFriendsToGameRequest
    {
        public List<string> FriendIds = new List<string>();
        public string GameId { get; set; }
        public string InviterId { get; set; }
    }

    public class InviteFriendsToGameResponse
    {
        public ResponseCodes Status { get; set; }

        public InviteFriendsToGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}