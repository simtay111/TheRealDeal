using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class AddTeamToGameRequestHandler : IHandler<AddTeamToGameRequest, AddTeamToGameResponse>
    {
        private readonly IGameRepository _gameRepository;

        public AddTeamToGameRequestHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public AddTeamToGameResponse Handle(AddTeamToGameRequest request)
        {
            var game = _gameRepository.GetById(request.GameId) as GameWithTeams;
            if (game == null) return new AddTeamToGameResponse(ResponseCodes.CannotHaveTeams);

            game.AddTeam(request.TeamId);

            _gameRepository.SaveOrUpdate(game);

            return new AddTeamToGameResponse(ResponseCodes.Success);
        }
    }

    public class AddTeamToGameRequest
    {
        public string TeamId;
        public string GameId;
    }

    public class AddTeamToGameResponse
    {
        public ResponseCodes Status { get; set; }

        public AddTeamToGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}