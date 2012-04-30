namespace RecreateMe.Divisions.Handlers
{
    public class AddTeamToDivisionRequestHandle : IHandle<AddTeamToDivisionRequest, AddTeamToDivisionResponse>
    {
        private readonly IDivisionRepository _divisionRepository;

        public AddTeamToDivisionRequestHandle(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public AddTeamToDivisionResponse Handle(AddTeamToDivisionRequest request)
        {
            var division = _divisionRepository.GetById(request.DivisionId);

            if (division.TeamIds.Contains(request.TeamId))
                return new AddTeamToDivisionResponse {Status = ResponseCodes.DuplicateEntryFound};

            _divisionRepository.AddTeamToDivision(division, request.TeamId);

            return new AddTeamToDivisionResponse();
        }
    }

    public class AddTeamToDivisionRequest
    {
        public string TeamId { get; set; }

        public string DivisionId { get; set; }
    }

    public class AddTeamToDivisionResponse
    {
        public ResponseCodes Status { get; set; }
    }
}