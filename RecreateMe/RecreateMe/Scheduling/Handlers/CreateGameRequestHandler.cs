using System;
using RecreateMe.Exceptions;
using RecreateMe.Exceptions.Formatting;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers
{
    public class CreateGameRequestHandler : IHandler<CreateGameRequest, CreateGameResponse>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGameFactory _gameFactory;
        private readonly ISportRepository _sportRepository;

        public CreateGameRequestHandler(ISportRepository sportRepository, ILocationRepository locationRepository, IGameRepository gameRepository, IGameFactory gameFactory)
        {
            _sportRepository = sportRepository;
            _locationRepository = locationRepository;
            _gameRepository = gameRepository;
            _gameFactory = gameFactory;
        }

        public CreateGameResponse Handle(CreateGameRequest request)
        {
            var status = CheckForNullsAndBadDateTimes(request);
            if (status != ResponseCodes.Success)
                return new CreateGameResponse(status);

            var sport = _sportRepository.FindByName(request.Sport);
            var location = _locationRepository.FindByName(request.Location);
            var dateTime = DateTime.Parse(request.DateTime);

            var game = request.HasTeams
                           ? _gameFactory.CreateGameWithTeams(dateTime, sport, location, request.IsPrivate)
                           : _gameFactory.CreateGameWithOutTeams(dateTime, sport, location, request.IsPrivate) as
                             Game;
            game.MaxPlayers = request.MaxPlayers;
            game.MinPlayers = request.MinPlayers;

            _gameRepository.Save(game);

            return new CreateGameResponse(ResponseCodes.Success);
        }

        private ResponseCodes CheckForNullsAndBadDateTimes(CreateGameRequest request)
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

    public class CreateGameRequest
    {
        public string DateTime { get; set; }
        public string Sport { get; set; }
        public string Location { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public bool HasTeams = true;
        public bool IsPrivate = false;
    }

    public class CreateGameResponse
    {
        public ResponseCodes Status { get; set; }

        public CreateGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}