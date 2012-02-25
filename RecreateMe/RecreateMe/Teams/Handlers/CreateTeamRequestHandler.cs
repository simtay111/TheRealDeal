using RecreateMe.Exceptions;

namespace RecreateMe.Teams.Handlers
{
    public class CreateTeamRequestHandler : IHandler<CreateTeamRequest, CreateTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public CreateTeamRequestHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public CreateTeamResponse Handle(CreateTeamRequest request)
        {
            if (request.Name == null)
                return new CreateTeamResponse(ResponseCodes.NameNotSpecified);

            var team = new Team
                           {
                               MaxSize = request.MaxSize == 0 ? Constants.DefaultTeamSize : request.MaxSize,
                               Name = request.Name
                           };

            _teamRepository.Save(team);

            return new CreateTeamResponse(ResponseCodes.Success);
        }
    }

    public class CreateTeamRequest
    {
        public int MaxSize { get; set; }
        public string Name { get; set; }
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