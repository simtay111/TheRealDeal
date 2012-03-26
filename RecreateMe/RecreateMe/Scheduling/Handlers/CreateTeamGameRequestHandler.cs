using System;
using RecreateMe.Locales;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;

namespace RecreateMe.Scheduling.Handlers
{
    public class CreateTeamGameRequestHandler : IHandler<CreateTeamGameRequest, CreateTeamGameResponse>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ITeamGameRepository _teamGameRepo;
        private readonly IGameFactory _gameFactory;
        private readonly ISportRepository _sportRepository;

        public CreateTeamGameRequestHandler(ISportRepository sportRepository, ILocationRepository locationRepository, ITeamGameRepository teamGameRepo, IGameFactory gameFactory)
        {
            _sportRepository = sportRepository;
            _locationRepository = locationRepository;
            _teamGameRepo = teamGameRepo;
            _gameFactory = gameFactory;
        }

        public CreateTeamGameResponse Handle(CreateTeamGameRequest request)
        {
            var status = CheckForNullsAndBadDateTimes(request);
            if (status != ResponseCodes.Success)
                return new CreateTeamGameResponse(status);

            var sport = _sportRepository.FindByName(request.Sport);
            var location = _locationRepository.FindByName(request.Location);
            var dateTime = DateTime.Parse(request.DateTime);

            var game = _gameFactory.CreateGameWithTeams(dateTime, sport, location, request.IsPrivate);
            game.MaxPlayers = request.MaxPlayers;
            game.MinPlayers = request.MinPlayers;
            game.Creator = request.Creator;

            _teamGameRepo.SaveTeamGame(game);

            return new CreateTeamGameResponse(ResponseCodes.Success) { GameId = game.Id };
        }

        private ResponseCodes CheckForNullsAndBadDateTimes(CreateTeamGameRequest request)
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

    public class CreateTeamGameRequest
    {
        public string DateTime { get; set; }
        public string Sport { get; set; }
        public string Location { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
        public string Creator { get; set; }

        public bool IsPrivate;
    }

    public class CreateTeamGameResponse
    {
        public ResponseCodes Status { get; set; }

        public string GameId { get; set; }

        public CreateTeamGameResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}