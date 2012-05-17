using System;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers
{
    public class CreatePickupGameRequestHandle : IHandle<CreatePickupGameRequest, CreatePickupGameResponse>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IPickUpGameRepository _pickUpGameRepository;
        private readonly IGameFactory _gameFactory;
        private readonly ISportRepository _sportRepository;

        public CreatePickupGameRequestHandle(ISportRepository sportRepository, ILocationRepository locationRepository, IPickUpGameRepository pickUpGameRepository, IGameFactory gameFactory)
        {
            _sportRepository = sportRepository;
            _locationRepository = locationRepository;
            _pickUpGameRepository = pickUpGameRepository;
            _gameFactory = gameFactory;
        }

        public CreatePickupGameResponse Handle(CreatePickupGameRequest request)
        {
            var status = CheckForNullsAndBadDateTimes(request);
            if (status != ResponseCodes.Success)
                return new CreatePickupGameResponse(status);

            var sport = _sportRepository.FindByName(request.Sport);
            var location = _locationRepository.FindByName(request.Location);
            var dateTime = DateTime.Parse(request.DateTime);

            var game = _gameFactory.CreatePickUpGame(dateTime, sport, location, request.IsPrivate);
            game.MaxPlayers = request.MaxPlayers;
            game.MinPlayers = request.MinPlayers;
            game.Creator = request.Creator;
            game.ExactLocation = request.ExactLocation;
            game.GameName = request.GameName;


            _pickUpGameRepository.SavePickUpGame(game);

            return new CreatePickupGameResponse(ResponseCodes.Success) { GameId = game.Id };
        }

        private ResponseCodes CheckForNullsAndBadDateTimes(CreatePickupGameRequest request)
        {
            if (request.Sport == null)
                return ResponseCodes.SportNotSpecified;

            if (request.Location == null)
                return ResponseCodes.LocationNotSpecified;

            if (request.DateTime == null)
                return ResponseCodes.DateNotSpecified;

            DateTime requestDateTime;
            if (!DateTime.TryParse(request.DateTime, out requestDateTime))
                return ResponseCodes.CouldNotParseDate;

            return ResponseCodes.Success;
        }
    }

    public class CreatePickupGameRequest
    {
        public string DateTime { get; set; }
        public string Sport { get; set; }
        public string Location { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public string Creator { get; set; }
        public string ExactLocation { get; set; }
        public string GameName { get; set; }

        public bool IsPrivate;
    }

    public class CreatePickupGameResponse
    {
        public ResponseCodes Status { get; set; }

        public string GameId { get; set; }

        public CreatePickupGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}