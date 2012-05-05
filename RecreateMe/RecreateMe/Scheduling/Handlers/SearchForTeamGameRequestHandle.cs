using System;
using System.Collections.Generic;
using System.Linq;
using RecreateMe.Scheduling.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class SearchForTeamGameRequestHandle : IHandle<SearchForTeamGameRequest, SearchForTeamGameResponse>
    {
        private readonly ITeamGameRepository _TeamGameRepository;

        public SearchForTeamGameRequestHandle(ITeamGameRepository TeamGameRepository)
        {
            _TeamGameRepository = TeamGameRepository;
        }

        public SearchForTeamGameResponse Handle(SearchForTeamGameRequest request)
        {
            if (String.IsNullOrEmpty(request.Location)) return new SearchForTeamGameResponse(ResponseCodes.LocationNotSpecified);

            var results = new List<TeamGame>();

            results.AddRange(_TeamGameRepository.FindTeamGameByLocation(request.Location));

            if (!string.IsNullOrEmpty(request.Sport))
               results = results.Where(x => x.Sport == request.Sport).ToList();

            return new SearchForTeamGameResponse(results);
        }
    }

    public class SearchForTeamGameRequest
    {
        public string Location { get; set; }
        public string Sport { get; set; }
    }

    public class SearchForTeamGameResponse
    {
        public IList<TeamGame> GamesFound;
        public ResponseCodes Status { get; set; }

        public SearchForTeamGameResponse(IList<TeamGame> gamesFound)
        {
            GamesFound = gamesFound;
            Status = ResponseCodes.Success;
        }

        public SearchForTeamGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}