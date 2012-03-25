using System.Linq;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class JoinGameRequestHandler: IHandler<JoinGameRequest, JoinGameResponse>
    {
        private readonly IGameRepository _gameRepository;

        public JoinGameRequestHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public JoinGameResponse Handle(JoinGameRequest request)
        {
            var game = GetValidGame(request.GameId);
            if (game.IsFull()) return new JoinGameResponse(ResponseCodes.GameIsFull);

            if (game.PlayersIds.Any(x => x == request.ProfileId))
                return new JoinGameResponse(ResponseCodes.AlreadyInGame);

            _gameRepository.AddPlayerToGame(game.Id, request.ProfileId);
             
            return new JoinGameResponse(ResponseCodes.Success);
        }

        private PickUpGame GetValidGame(string gameId)
        {
            return _gameRepository.GetPickUpGameById(gameId);
        }
    }

    public class JoinGameRequest
    {
        public string GameId { get; set; }
        public string ProfileId { get; set; }
    }

    public class JoinGameResponse
    {
        public ResponseCodes Status { get; set; }

        public JoinGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}