namespace RecreateMe.Scheduling.Handlers
{
    public class DeleteTeamGameRequestHandler : IHandler<DeleteTeamGameRequest, DeleteTeamGameResponse>
    {
        private readonly ITeamGameRepository _teamGameRepository;

        public DeleteTeamGameRequestHandler(ITeamGameRepository teamGameRepository)
        {
            _teamGameRepository = teamGameRepository;
        }

        public DeleteTeamGameResponse Handle(DeleteTeamGameRequest request)
        {
            var game = _teamGameRepository.GetTeamGameById(request.GameId);

            if (game.Creator != request.ProfileId)
                return new DeleteTeamGameResponse {Status = ResponseCodes.NotCreator};

            _teamGameRepository.DeleteGame(request.GameId);

            return new DeleteTeamGameResponse();
        }
    }

    public class DeleteTeamGameRequest
    {
        public string GameId { get; set; }

        public string ProfileId { get; set; }
    }

    public class DeleteTeamGameResponse
    {
        public ResponseCodes Status { get; set; }
    }
}