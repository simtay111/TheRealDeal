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
            _teamGameRepository.DeleteGame(request.GameId);

            return new DeleteTeamGameResponse();
        }
    }

    public class DeleteTeamGameRequest
    {
        public string GameId { get; set; }
    }

    public class DeleteTeamGameResponse
    {
    }
}