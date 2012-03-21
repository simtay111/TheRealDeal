using System.Linq;

using RecreateMe.Sports;

namespace RecreateMe.Profiles.Handlers
{
    public class AddSportToProfileRequestHandler : IHandler<AddSportToProfileRequest, AddSportToProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ISportRepository _sportRepository;

        public AddSportToProfileRequestHandler(IProfileRepository profileRepository, ISportRepository sportRepository)
        {
            _profileRepository = profileRepository;
            _sportRepository = sportRepository;
        }

        public AddSportToProfileResponse Handle(AddSportToProfileRequest request)
        {
            if (request.Sport == null)
                return new AddSportToProfileResponse(ResponseCodes.SportNotSpecified);

            var profile = _profileRepository.GetByProfileId(request.UniqueId);

            var sport = CreateSportWithSkillLevelFromRequest(request);

            if (profile.SportsPlayed.Any(x => x.Name == sport.Name))
                return new AddSportToProfileResponse(ResponseCodes.SportAlreadyPlayed);

            _profileRepository.AddSportToProfile(profile, sport);

            return new AddSportToProfileResponse(ResponseCodes.Success);
        }

        private SportWithSkillLevel CreateSportWithSkillLevelFromRequest(AddSportToProfileRequest request)
        {
            var sport = _sportRepository.FindByName(request.Sport);

            var sportToAdd = new SportWithSkillLevel
            {
                Name = sport.Name,
                SkillLevel = new SkillLevel(request.SkillLevel)
            };


            return sportToAdd;
        }
    }

    public class AddSportToProfileRequest
    {
        public string Sport;
        public int SkillLevel;
        public string UniqueId;
    }

    public class AddSportToProfileResponse
    {
        public ResponseCodes Status { get; set; }

        public AddSportToProfileResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}