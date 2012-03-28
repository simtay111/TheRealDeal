using System.Collections.Generic;
using RecreateMe.Scheduling.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class GetGamesForProfileRequestHandler : IHandler<GetGamesForProfileRequest, GetGamesForProfileResponse>
    {
        private readonly IPickUpGameRepository _pickUpGameRepository;
        private readonly ITeamGameRepository _teamGameRepo;

        public GetGamesForProfileRequestHandler(IPickUpGameRepository pickUpGameRepository, ITeamGameRepository teamGameRepo)
        {
            _pickUpGameRepository = pickUpGameRepository;
            _teamGameRepo = teamGameRepo;
        }

        public GetGamesForProfileResponse Handle(GetGamesForProfileRequest request)
        {
            var pickUpGames = _pickUpGameRepository.GetPickupGamesForProfile(request.ProfileId);
            var teamGames = _teamGameRepo.GetTeamGamesForProfile(request.ProfileId);

            return new GetGamesForProfileResponse {PickupGames = pickUpGames, TeamGames = teamGames};
        }
    }

    public class GetGamesForProfileRequest
    {
        public string ProfileId { get; set; }
    }
    public class GetGamesForProfileResponse
    {
        public IList<PickUpGame> PickupGames { get; set; }
        public IList<TeamGame> TeamGames { get; set; }
    }
}