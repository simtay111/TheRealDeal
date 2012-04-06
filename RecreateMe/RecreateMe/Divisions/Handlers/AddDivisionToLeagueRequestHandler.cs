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

            league.DivisionIds.Add(request.DivisionId);

            _leagueRepository.Save(league);

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
    }
}