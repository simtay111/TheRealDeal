namespace RecreateMe.Divisions.Handlers
{
    public class AddTeamToDivisionRequestHandler : IHandler<AddTeamToDivisionRequest, AddTeamToDivisionResponse>
    {
        private readonly IDivisionRepository _divisionRepository;

        public AddTeamToDivisionRequestHandler(IDivisionRepository divisionRepository)
        {
            _divisionRepository = divisionRepository;
        }

        public AddTeamToDivisionResponse Handle(AddTeamToDivisionRequest request)
        {
            var division = _divisionRepository.GetById(request.DivisionId);

            division.TeamIds.Add(request.TeamId);

            _divisionRepository.Save(division);

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
    }
}