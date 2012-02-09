using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Teams;

namespace RecreateMe.Scheduling.Handlers
{
    public class AddTeamToGameRequestHandler : IHandler<AddTeamToGameRequest, AddTeamToGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;

        public AddTeamToGameRequestHandler(IGameRepository gameRepository, ITeamRepository teamRepository)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
        }

        public AddTeamToGameResponse Handle(AddTeamToGameRequest request)
        {
            var game = _gameRepository.GetById(request.GameId) as GameWithTeams;
            if (game == null) return new AddTeamToGameResponse(ResponseCodes.CannotHaveTeams);

            var team = _teamRepository.GetById(request.TeamId);

            game.AddTeam(team);

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