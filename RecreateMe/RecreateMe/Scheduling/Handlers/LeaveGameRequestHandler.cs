namespace RecreateMe.Scheduling.Handlers
{
    public class LeaveGameRequestHandler : IHandle<LeaveGameRequest, LeaveGameResponse>
    {
        private readonly IPickUpGameRepository _pickUpGameRepository;

        public LeaveGameRequestHandler(IPickUpGameRepository pickUpGameRepository)
        {
            _pickUpGameRepository = pickUpGameRepository;
        }

        public LeaveGameResponse Handle(LeaveGameRequest request)
        {
            _pickUpGameRepository.RemovePlayerFromGame(request.ProfileId, request.GameId);

            return new LeaveGameResponse();
        }
    }

    public class LeaveGameRequest
    {
        public string GameId { get; set; }

        public string ProfileId { get; set; }
    }

    public class LeaveGameResponse
    {
        public ResponseCodes Status { get; set; }
    }
}