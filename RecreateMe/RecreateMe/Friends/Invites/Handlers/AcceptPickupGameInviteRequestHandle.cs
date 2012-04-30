using RecreateMe.Scheduling;

namespace RecreateMe.Friends.Invites.Handlers
{
    public class AcceptPickupGameInviteRequestHandle : IHandle<AcceptPickupGameRequest, AcceptPickupGameResponse>
    {
        private readonly IPickUpGameRepository _pickUpGameRepository;
        private readonly IInviteRepository _inviteRepository;

        public AcceptPickupGameInviteRequestHandle(IPickUpGameRepository pickUpGameRepository, IInviteRepository inviteRepository)
        {
            _pickUpGameRepository = pickUpGameRepository;
            _inviteRepository = inviteRepository;
        }

        public AcceptPickupGameResponse Handle(AcceptPickupGameRequest request)
        {
            _inviteRepository.Delete(request.InviteId);

            var game = _pickUpGameRepository.GetPickUpGameById(request.GameId);

            if (game.IsFull())
                return new AcceptPickupGameResponse { Status = ResponseCodes.GameIsFull };

            _pickUpGameRepository.AddPlayerToGame(request.GameId, request.ProfileId);

            return new AcceptPickupGameResponse { Status = ResponseCodes.Success };
        }
    }   
    
    public class AcceptPickupGameRequest{
        public string ProfileId { get; set; }

        public string GameId { get; set; }

        public string InviteId { get; set; }
    }
    
    public class AcceptPickupGameResponse
    {
        public ResponseCodes Status { get; set; }
    }
}