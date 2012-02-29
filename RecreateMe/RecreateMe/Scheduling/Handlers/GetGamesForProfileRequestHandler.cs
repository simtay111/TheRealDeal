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
            var games = _gameRepository.GetForProfile(request.ProfileId);

            return new GetGamesForProfileResponse {Games = games};
        }
    }

    public class GetGamesForProfileRequest
    {
        public string ProfileId { get; set; }
    }
    public class GetGamesForProfileResponse
    {
        public IList<Game> Games { get; set; }
    }
}