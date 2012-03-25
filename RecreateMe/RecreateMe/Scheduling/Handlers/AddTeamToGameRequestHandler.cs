
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
            var game = _gameRepository.GetTeamGameById(request.GameId);
            if (game.IsFull()) return new AddTeamToGameResponse(ResponseCodes.GameIsFull);

            _gameRepository.AddTeamToGame(request.TeamId, request.GameId);

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