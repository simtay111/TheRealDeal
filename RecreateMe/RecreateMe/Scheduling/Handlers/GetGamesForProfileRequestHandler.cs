using System.Collections.Generic;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class GetGamesForProfileRequestHandler : IHandler<GetGamesForProfileRequest, GetGamesForProfileResponse>
    {
        private readonly IGameRepository _gameRepository;

        public GetGamesForProfileRequestHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GetGamesForProfileResponse Handle(GetGamesForProfileRequest request)
        {
            var pickUpGames = _gameRepository.GetPickupGamesForProfile(request.ProfileId);
            var teamGames = _gameRepository.GetTeamGamesForProfile(request.ProfileId);

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
        public IList<GameWithTeams> TeamGames { get; set; }
    }
}