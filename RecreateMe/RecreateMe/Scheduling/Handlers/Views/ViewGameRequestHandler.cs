using System.Collections.Generic;
using System.Linq;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers.Views
{
    public class ViewGameRequestHandler : IHandle<ViewGameRequest, ViewGameResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IPickUpGameRepository _pickUpGameRepository;

        public ViewGameRequestHandler(IProfileRepository profileRepository, IPickUpGameRepository pickUpGameRepository)
        {
            _profileRepository = profileRepository;
            _pickUpGameRepository = pickUpGameRepository;
        }

        public ViewGameResponse Handle(ViewGameRequest request)
        {
            var game = _pickUpGameRepository.GetPickUpGameById(request.GameId);

            var profiles = _profileRepository.GetProfilesInGame(request.GameId);

            var dict = profiles.ToDictionary(profile => profile.ProfileId,
                profile => GetSkillLevelForGame(game, profile));


            return new ViewGameResponse {Game = game, ProfilesAndSkillLevels = dict};
        }

        private static int GetSkillLevelForGame(PickUpGame game, Profile profile)
        {
            var sport = profile.SportsPlayed.SingleOrDefault(x => x.Name == game.Sport.Name);

            var skillLevel = sport != null ? sport.SkillLevel.Level : Constants.DefaultSkillLevel;
            return skillLevel;
        }
    }

    public class ViewGameRequest
    {
        public string GameId { get; set; }
    }

    public class ViewGameResponse
    {
        public PickUpGame Game { get; set; }

        public Dictionary<string, int> ProfilesAndSkillLevels { get; set; }
    }
}