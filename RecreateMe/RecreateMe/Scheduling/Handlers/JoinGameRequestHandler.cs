using RecreateMe.Exceptions;
using RecreateMe.Exceptions.Scheduling;
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

            game.PlayersIds.Add(request.ProfileId);

            _gameRepository.SaveOrUpdate(game);
             
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