using RecreateMe.Leagues;

namespace RecreateMe.Divisions.Handlers
{
    public class AddDivisionToLeagueRequestHandler : IHandler<AddDivisionToLeagueRequest, AddDivisionToLeagueResponse>
    {
        private readonly ILeagueRepository _leagueRepository;

        public AddDivisionToLeagueRequestHandler(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public AddDivisionToLeagueResponse Handle(AddDivisionToLeagueRequest request)
        {
            var league = _leagueRepository.GetById(request.LeagueId);
            if (league.DivisionIds.Contains(request.DivisionId))
                return new AddDivisionToLeagueResponse() {Status = ResponseCodes.AlreadyInLeague};

            _leagueRepository.AddDivisionToLeague(league, request.DivisionId);

            return new AddDivisionToLeagueResponse();
        }
    }

    public class AddDivisionToLeagueRequest
    {
        public string LeagueId { get; set; }
        public string DivisionId { get; set; }
    }

    public class AddDivisionToLeagueResponse
    {
        public ResponseCodes Status { get; set; }
    }
}