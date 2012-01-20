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
            if (string.IsNullOrEmpty(request.Name)) return new CreateProfileResponse(ResponseCodes.NameNotSpecified);

            var profile = BuildProfileFromRequest(request);

            _profileRepository.SaveOrUpdate(profile);

            return new CreateProfileResponse(ResponseCodes.Success);
        }

        private Profile BuildProfileFromRequest(CreateProfileRequest request)
        {
            var skill = CreateSkillLevel(request);
            var sport = request.Sports != "" ? _sportRepository.FindByName(request.Sports) : null ;
            var location = request.Location != "" ? _locationRepository.FindByName(request.Location) : null;
            var name = NameParser.CreateName(request.Name);

            var profile = CreateProfile(sport, skill, name, location);
            profile.UserId = request.UserId;
            return profile;
        }

        private SkillLevel CreateSkillLevel(CreateProfileRequest request)
        {
            var skillLevel = new SkillLevel();
            if (request.SkillLevel != "")
                skillLevel.Level = int.Parse(request.SkillLevel);
            return skillLevel;
        }

        private Profile CreateProfile(Sport sport, SkillLevel skill, Name name, Location location)
        {
            var person = _profileBuilder.WithName(name)
                .WithLocation(location)
                .WithSport(sport)
                .WithSkillLevel(skill)
                .Build();
            return person;
        }
    }

    public class CreateProfileRequest
    {
        public CreateProfileRequest(string userId, string name, string location = "", string sports = "", string skillLevel = "")
        {
            UserId = userId;
            Name = name;
            Location = location;
            Sports = sports;
            SkillLevel = skillLevel;
        }

        public readonly string UserId;
        public readonly string Name;
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