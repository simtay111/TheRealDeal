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
            if (game == null) return new JoinGameResponse(ResponseCodes.OnlyTeamsCanJoin);
            if (game.IsFull()) return new JoinGameResponse(ResponseCodes.GameIsFull);

            if (game.PlayersIds.Any(x => x == request.ProfileId))
                return new JoinGameResponse(ResponseCodes.AlreadyInGame);

            _gameRepository.AddPlayerToGame(game.Id, request.ProfileId);
             
            return new JoinGameResponse(ResponseCodes.Success);
        }

        private GameWithoutTeams GetValidGame(string gameId)
        {
            return _gameRepository.GetById(gameId) as GameWithoutTeams;
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