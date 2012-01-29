using RecreateMe.Exceptions;
using RecreateMe.Locales;

namespace RecreateMe.Profiles.Handlers
{
    public class AddLocationToProfileRequestHandler : IHandler<AddLocationToProfileRequest, AddLocationToProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILocationRepository _locationRepository;

        public AddLocationToProfileRequestHandler(IProfileRepository profileRepository, ILocationRepository locationRepository)
        {
            _profileRepository = profileRepository;
            _locationRepository = locationRepository;
        }

        public AddLocationToProfileResponse Handle(AddLocationToProfileRequest request)
        {
            var profile = _profileRepository.GetByProfileId(request.ProfileId);

            if (request.Location == null) return new AddLocationToProfileResponse(ResponseCodes.LocationNotSpecified);
            var location = _locationRepository.FindByName(request.Location);

            if (location == null) return new AddLocationToProfileResponse(ResponseCodes.LocationNotFound);

            profile.Locations.Add(location);

            _profileRepository.SaveOrUpdate(profile);

            return new AddLocationToProfileResponse(ResponseCodes.Success);
        }
    }

    public class AddLocationToProfileRequest
    {
        public string ProfileId { get; set; }
        public string Location { get; set; }
    }

    public class AddLocationToProfileResponse
    {
        public ResponseCodes Status { get; set; }

        public AddLocationToProfileResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}