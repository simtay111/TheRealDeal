using System.Linq;
using RecreateMe.Profiles;

namespace RecreateMe.Friends.Handlers
{
    public class AddPlayerToFriendsRequestHandle : IHandle<AddPlayerToFriendsRequest, AddPlayerToFriendsResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public AddPlayerToFriendsRequestHandle(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public AddPlayerToFriendsResponse Handle(AddPlayerToFriendsRequest request)
        {
            var profile = _profileRepository.GetByProfileId(request.ProfileId);
            if (profile.FriendsIds.Any(x => x == request.FriendId))
                return new AddPlayerToFriendsResponse(ResponseCodes.AlreadyFriend);

            var friendProfile = _profileRepository.GetByProfileId(request.FriendId);

            profile.FriendsIds.Add(friendProfile.ProfileId);

            _profileRepository.AddFriendToProfile(profile.ProfileId, friendProfile.ProfileId);

            return new AddPlayerToFriendsResponse(ResponseCodes.Success);
        }
    }

    public class AddPlayerToFriendsRequest
    {
        public string FriendId { get; set; }
        public string ProfileId { get; set; }
    }

    public class AddPlayerToFriendsResponse
    {
        public ResponseCodes Status;

        public AddPlayerToFriendsResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}