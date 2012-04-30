using System.Collections.Generic;
using RecreateMe.Profiles;

namespace RecreateMe.Friends.Handlers
{
    public class GetFriendsRequestHandle : IHandle<GetFriendsRequestHandlerRequest, GetFriendsRequestHandlerResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public GetFriendsRequestHandle(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public GetFriendsRequestHandlerResponse Handle(GetFriendsRequestHandlerRequest request)
        {
            var friendsList = _profileRepository.GetFriendsProfileNameList(request.ProfileId);

            return new GetFriendsRequestHandlerResponse {FriendsNamesAndIds = friendsList};
        }
    }

    public class GetFriendsRequestHandlerRequest
    {
        public string ProfileId { get; set; }
    }

    public class GetFriendsRequestHandlerResponse
    {
        public IList<string> FriendsNamesAndIds { get; set; }
    }
}