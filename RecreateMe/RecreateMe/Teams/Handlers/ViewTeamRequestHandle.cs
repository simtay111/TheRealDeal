﻿namespace RecreateMe.Teams.Handlers
{
    public class ViewTeamRequestHandle : IHandle<ViewTeamRequest, ViewTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public ViewTeamRequestHandle(ITeamRepository teamRepository)
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