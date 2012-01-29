using RecreateMe.Profiles;

namespace RecreateMe.Friends.Handlers
{
    public class AddPlayerToFriendsRequestHandler : IHandler<AddPlayerToFriendsRequest, AddPlayerToFriendsResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public AddPlayerToFriendsRequestHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public AddPlayerToFriendsResponse Handle(AddPlayerToFriendsRequest request)
        {
            var profile = _profileRepository.GetByProfileId(request.ProfileId);

            profile.FriendsIds.Add(request.FriendId);

            _profileRepository.SaveOrUpdate(profile);

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