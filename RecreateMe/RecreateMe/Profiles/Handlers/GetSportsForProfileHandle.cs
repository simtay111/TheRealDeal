using System.Collections.Generic;
using RecreateMe.Sports;

namespace RecreateMe.Profiles.Handlers
{
    public class GetSportsForProfileHandle : IHandle<GetSportsForProfileRequest, GetSportsForProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public GetSportsForProfileHandle(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public GetSportsForProfileResponse Handle(GetSportsForProfileRequest request)
        {
            var profile = _profileRepository.GetByProfileId(request.ProfileId);

            return new GetSportsForProfileResponse {SportsForProfile = profile.SportsPlayed};
        }
    }

    public class GetSportsForProfileResponse
    {
        public IList<SportWithSkillLevel> SportsForProfile { get; set; }
    }

    public class GetSportsForProfileRequest
    {
        public string ProfileId { get; set; }
    }
}