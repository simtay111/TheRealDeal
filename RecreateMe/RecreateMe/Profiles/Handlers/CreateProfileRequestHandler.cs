using RecreateMe.Exceptions;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles.Handlers
{
    public class CreateProfileRequestHandler : IHandler<CreateProfileRequest, CreateProfileResponse>
    {
        private readonly ISportRepository _sportRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileBuilder _profileBuilder;

        public CreateProfileRequestHandler(ISportRepository sportRepository, ILocationRepository locationRepository, IProfileRepository profileRepository, IProfileBuilder profileBuilder)
        {
            _sportRepository = sportRepository;
            _locationRepository = locationRepository;
            _profileRepository = profileRepository;
            _profileBuilder = profileBuilder;
        }

        public CreateProfileResponse Handle(CreateProfileRequest request)
        {
            if (AtMaxAmountOfProfiles(request.UserId)) return new CreateProfileResponse(ResponseCodes.MaxProfilesReached);
            if (string.IsNullOrEmpty(request.ProfileId)) return new CreateProfileResponse(ResponseCodes.NameNotSpecified);
            if (ProfileNameAlreadyExists(request.ProfileId)) return new CreateProfileResponse(ResponseCodes.ProfileNameAlreadyExists);

            var profile = BuildProfileFromRequest(request);

            _profileRepository.Save(profile);

            return new CreateProfileResponse(ResponseCodes.Success);
        }

        private bool ProfileNameAlreadyExists(string profileName)
        {
            return _profileRepository.ProfileExistsWithName(profileName);    
        }

        private bool AtMaxAmountOfProfiles(string userId)
        {
            var profileCount = _profileRepository.GetByAccount(userId).Count;

            if (profileCount < Constants.MaxNumberOfProfilesPerAccount) return false;
            return true;
        }

        private Profile BuildProfileFromRequest(CreateProfileRequest request)
        {
            var skill = CreateSkillLevel(request);
            var sport = request.Sports != "" ? _sportRepository.FindByName(request.Sports) : null ;
            var location = request.Location != "" ? _locationRepository.FindByName(request.Location) : null;
            var profileId = request.ProfileId;

            var profile = CreateProfile(sport, skill, profileId, location);
            profile.AccountId = request.UserId;
            return profile;
        }

        private SkillLevel CreateSkillLevel(CreateProfileRequest request)
        {
            var skillLevel = new SkillLevel();
            if (!string.IsNullOrEmpty(request.SkillLevel))
                skillLevel.Level = int.Parse(request.SkillLevel);
            return skillLevel;
        }

        private Profile CreateProfile(Sport sport, SkillLevel skill, string profileId, Location location)
        {
            var person = _profileBuilder.WithProfileId(profileId)
                .WithLocation(location)
                .WithSport(sport)
                .WithSkillLevel(skill)
                .Build();
            return person;
        }
    }

    public class CreateProfileRequest
    {
        public CreateProfileRequest(string userId, string profileId, string location = "", string sports = "", string skillLevel = "")
        {
            UserId = userId;
            ProfileId = profileId;
            Location = location;
            Sports = sports;
            SkillLevel = skillLevel;
        }

        public readonly string UserId;
        public readonly string ProfileId;
        public readonly string Location;
        public readonly string Sports;
        public readonly string SkillLevel;
    }

    public class CreateProfileResponse
    {
        public ResponseCodes Status { get; set; }

        public CreateProfileResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}