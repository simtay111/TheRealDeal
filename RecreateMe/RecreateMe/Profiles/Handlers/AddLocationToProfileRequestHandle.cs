using System.Linq;

using RecreateMe.Locales;

namespace RecreateMe.Profiles.Handlers
{
    public class AddLocationToProfileRequestHandle : IHandle<AddLocationToProfileRequest, AddLocationToProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ILocationRepository _locationRepository;

        public AddLocationToProfileRequestHandle(IProfileRepository profileRepository, ILocationRepository locationRepository)
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

            if (profile.Locations.Any(x => x.Name == request.Location))
                return new AddLocationToProfileResponse(ResponseCodes.LocationAlreadyInProfile);

            profile.Locations.Add(location);

            _profileRepository.AddLocationToProfile(profile, location);

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