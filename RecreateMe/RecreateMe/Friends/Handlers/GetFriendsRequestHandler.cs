using System.Collections.Generic;
using RecreateMe.Profiles;

namespace RecreateMe.Friends.Handlers
{
    public class GetFriendsRequestHandler : IHandler<GetFriendsRequestHandlerRequest, GetFriendsRequestHandlerResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public GetFriendsRequestHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public GetFriendsRequestHandlerResponse Handle(GetFriendsRequestHandlerRequest request)
        {
            var friendsList = _profileRepository.GetFriendIdAndNameListForProfile(request.ProfileId);

            return new GetFriendsRequestHandlerResponse {FriendsNamesAndIds = friendsList};
        }
    }

    public class GetFriendsRequestHandlerRequest
    {
        public string ProfileId { get; set; }
    }

    public class GetFriendsRequestHandlerResponse
    {
        public Dictionary<string, Name> FriendsNamesAndIds { get; set; }
    }
}