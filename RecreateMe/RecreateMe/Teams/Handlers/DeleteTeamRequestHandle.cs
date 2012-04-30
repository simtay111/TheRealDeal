namespace RecreateMe.Teams.Handlers
{
    public class DeleteTeamRequestHandle : IHandle<DeleteTeamRequest, DeleteTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamRequestHandle(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public DeleteTeamResponse Handle(DeleteTeamRequest request)
        {
            var team = _teamRepository.GetById(request.TeamId);

            if (team.Creator != request.ProfileId)
                return new DeleteTeamResponse {Status = ResponseCodes.NotCreator};

            _teamRepository.DeleteTeam(request.TeamId);

            return new DeleteTeamResponse();
        }
    }

    public class DeleteTeamRequest
    {
        public string TeamId { get; set; }

        public string ProfileId { get; set; }
    }

    public class DeleteTeamResponse
    {
        public ResponseCodes Status { get; set; }
    }
}