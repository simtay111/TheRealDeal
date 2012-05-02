using System.Collections.Generic;
using System.Linq;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;

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

            var dict = profiles.ToDictionary(profile => profile.ProfileId, profile => profile.SportsPlayed.Single(x => x.Name == game.Sport.Name).SkillLevel.Level);

            return new ViewGameResponse {Game = game, ProfilesAndSkillLevels = dict};
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