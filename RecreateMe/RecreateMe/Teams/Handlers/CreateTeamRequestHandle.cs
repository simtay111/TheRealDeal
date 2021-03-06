
namespace RecreateMe.Teams.Handlers
{
    public class CreateTeamRequestHandle : IHandle<CreateTeamRequest, CreateTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamRequestHandle(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public CreateTeamResponse Handle(CreateTeamRequest request)
        {
            if (request.Name == null)
                return new CreateTeamResponse(ResponseCodes.NameNotSpecified);
            if (request.ProfileId == null)
                return new CreateTeamResponse(ResponseCodes.ProfileIdRequired);

            var team = new Team
                           {
                               MaxSize = request.MaxSize == 0 ? Constants.DefaultTeamSize : request.MaxSize,
                               Name = request.Name,
                               Creator = request.ProfileId
                           };

            team.PlayersIds.Add(request.ProfileId);

            _teamRepository.Save(team);

            return new CreateTeamResponse(ResponseCodes.Success);
        }
    }

    public class CreateTeamRequest
    {
        public int MaxSize { get; set; }
        public string Name { get; set; }

        public string ProfileId { get; set; }
    }

    public class CreateTeamResponse
    {
        public ResponseCodes Status { get; set; }

        public CreateTeamResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}