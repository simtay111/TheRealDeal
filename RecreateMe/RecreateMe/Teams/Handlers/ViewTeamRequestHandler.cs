namespace RecreateMe.Teams.Handlers
{
    public class ViewTeamRequestHandler : IHandler<ViewTeamRequest, ViewTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public ViewTeamRequestHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public ViewTeamResponse Handle(ViewTeamRequest request)
        {
            return new ViewTeamResponse {Team = _teamRepository.GetById(request.TeamId)};
        }
    }
    
    public class ViewTeamRequest
    {
        public string TeamId { get; set; }
    }

    public class ViewTeamResponse
    {
        public Team Team { get; set; }
    }
}