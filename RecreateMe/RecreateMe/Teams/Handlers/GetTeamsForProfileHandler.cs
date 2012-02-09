using System.Collections.Generic;

namespace RecreateMe.Teams.Handlers
{
    public class GetTeamsForProfileHandler : IHandler<GetTeamsForProfileRequest, GetTeamsForProfileResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamsForProfileHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public GetTeamsForProfileResponse Handle(GetTeamsForProfileRequest request)
        {
            var response = new GetTeamsForProfileResponse()
                               {
                                   Teams = _teamRepository.GetTeamsForProfile(request.ProfileId)
                               };

            return response;
        }
    }

    public class GetTeamsForProfileRequest
    {
        public string ProfileId { get; set; }
    }

    public class GetTeamsForProfileResponse
    {
        public IList<Team> Teams { get; set; }
    }
}