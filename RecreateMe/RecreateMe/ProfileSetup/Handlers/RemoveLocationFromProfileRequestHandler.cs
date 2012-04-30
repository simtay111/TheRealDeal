using RecreateMe.Profiles;

namespace RecreateMe.ProfileSetup.Handlers
{
    public class RemoveLocationFromProfileRequestHandler : IHandle<RemoveLocationFromProfileRequest, RemoveLocationFromProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public RemoveLocationFromProfileRequestHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public RemoveLocationFromProfileResponse Handle(RemoveLocationFromProfileRequest request)
        {
            _profileRepository.RemoveLocationFromProfile(request.ProfileId, request.LocationName);

            return new RemoveLocationFromProfileResponse();
        }
    }

    public class RemoveLocationFromProfileRequest
    {
        public string ProfileId { get; set; }

        public string LocationName { get; set; }
    }

    public class RemoveLocationFromProfileResponse
    {
        public ResponseCodes Status { get; set; }
    }
}