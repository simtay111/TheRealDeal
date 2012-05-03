using System;
using System.Collections.Generic;
using System.Linq;
using RecreateMe.Scheduling.Games;

namespace RecreateMe.Scheduling.Handlers
{
    public class SearchForPickupGameRequestHandle : IHandle<SearchForPickupGameRequest, SearchForPickUpGameResponse>
    {
        private readonly IPickUpGameRepository _pickUpGameRepository;

        public SearchForPickupGameRequestHandle(IPickUpGameRepository pickUpGameRepository)
        {
            _pickUpGameRepository = pickUpGameRepository;
        }

        public SearchForPickUpGameResponse Handle(SearchForPickupGameRequest request)
        {
            if (String.IsNullOrEmpty(request.Location)) return new SearchForPickUpGameResponse(ResponseCodes.LocationNotSpecified);

            var results = new List<PickUpGame>();

            results.AddRange(_pickUpGameRepository.FindPickUpGameByLocation(request.Location).Where(x => !x.PlayersIds.Contains(request.ProfileId)));

            if (!string.IsNullOrEmpty(request.Sport))
               results = results.Where(x => x.Sport.Name == request.Sport).ToList();

            return new SearchForPickUpGameResponse(results);
        }
    }

    public class SearchForPickupGameRequest
    {
        public string Location { get; set; }
        public string Sport { get; set; }
        public string ProfileId { get; set; }
    }

    public class SearchForPickUpGameResponse
    {
        public IList<PickUpGame> GamesFound;
        public ResponseCodes Status { get; set; }

        public SearchForPickUpGameResponse(IList<PickUpGame> gamesFound)
        {
            GamesFound = gamesFound;
            Status = ResponseCodes.Success;
        }

        public SearchForPickUpGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}