using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class AddTeamToGameRequestHandler : IHandler<AddTeamToGameRequest, AddTeamToGameResponse>
    {
        private readonly ITeamGameRepository _pickUpGameRepository;

        public AddTeamToGameRequestHandler(ITeamGameRepository pickUpGameRepository)
        {
            _pickUpGameRepository = pickUpGameRepository;
        }

        public AddTeamToGameResponse Handle(AddTeamToGameRequest request)
        {
            var game = _pickUpGameRepository.GetTeamGameById(request.GameId);
            if (game.IsFull()) return new AddTeamToGameResponse(ResponseCodes.GameIsFull);

            _pickUpGameRepository.AddTeamToGame(request.TeamId, request.GameId);

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