using RecreateMe.Profiles;

namespace RecreateMe.ProfileSetup.Handlers
{
    public class RemoveSportFromProfileRequestHandler : IHandle<RemoveSportFromProfileRequest, RemoveSportFromProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public RemoveSportFromProfileRequestHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public RemoveSportFromProfileResponse Handle(RemoveSportFromProfileRequest request)
        {
            _profileRepository.RemoveSportFromProfile(request.ProfileId, request.SportName);

            return new RemoveSportFromProfileResponse();
        }
    }

    public class RemoveSportFromProfileRequest
    {
        public string ProfileId { get; set; }

        public string SportName { get; set; }
    }

    public class RemoveSportFromProfileResponse
    {
        public ResponseCodes Status { get; set; }
    }
}