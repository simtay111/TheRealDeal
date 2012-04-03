
namespace RecreateMe.Leagues.Handlers
{
    public class CreateLeagueRequestHandler : IHandler<CreateLeagueRequest, CreateLeagueResponse>
    {
        private ILeagueRepository _leagueRepo;

        public CreateLeagueRequestHandler(ILeagueRepository leagueRepository)
        {
            _leagueRepo = leagueRepository;
        }

        public CreateLeagueResponse Handle(CreateLeagueRequest request)
        {
            var league = new League
                             {
                                 Name = request.Name
                             };

            _leagueRepo.Save(league);

            return new CreateLeagueResponse();
        }
    }

    public class CreateLeagueRequest
    {
        public string Name { get; set; }
    }

    public class CreateLeagueResponse
    {
    }
}