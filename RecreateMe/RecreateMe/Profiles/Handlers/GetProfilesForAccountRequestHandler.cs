using System.Collections.Generic;

namespace RecreateMe.Profiles.Handlers
{
    public class GetProfilesForAccountRequestHandler : IHandler<GetProfilesForAccountRequest, GetProfilesForAccountResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public GetProfilesForAccountRequestHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public GetProfilesForAccountResponse Handle(GetProfilesForAccountRequest request)
        {
            var response = new GetProfilesForAccountResponse();

            response.Profiles = _profileRepository.GetByAccount(request.Account);

            return response;
        }
    }

    public class GetProfilesForAccountResponse
    {
        public IList<Profile> Profiles { get; set; }
    }

    public class  GetProfilesForAccountRequest
    {
        public string Account { get; set; }
    }
}