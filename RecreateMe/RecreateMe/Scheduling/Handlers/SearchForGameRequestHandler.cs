using System;
using System.Collections.Generic;
using System.Linq;

using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class SearchForGameRequestHandler : IHandler<SearchForGameRequest, SearchForGameResponse>
    {
        private readonly IGameRepository _gameRepository;

        public SearchForGameRequestHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public SearchForGameResponse Handle(SearchForGameRequest request)
        {
            if (String.IsNullOrEmpty(request.Location)) return new SearchForGameResponse(ResponseCodes.LocationNotSpecified);

            var results = new List<Game>();

            results.AddRange(_gameRepository.FindByLocation(request.Location));

            if (!string.IsNullOrEmpty(request.Sport))
               results = results.Where(x => x.Sport.Name == request.Sport).ToList();

            return new SearchForGameResponse(results);
        }
    }

    public class SearchForGameRequest
    {
        public string Location { get; set; }
        public string Sport { get; set; }
    }

    public class SearchForGameResponse
    {
        public IList<Game> GamesFound;
        public ResponseCodes Status { get; set; }

        public SearchForGameResponse(IList<Game> gamesFound)
        {
            GamesFound = gamesFound;
            Status = ResponseCodes.Success;
        }

        public SearchForGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}