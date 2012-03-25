using System;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Friends.Invites.Handlers
{
    public class AcceptPickupGameInviteRequestHandler : IHandler<AcceptPickupGameRequest, AcceptPickupGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IInviteRepository _inviteRepository;

        public AcceptPickupGameInviteRequestHandler(IGameRepository gameRepository, IInviteRepository inviteRepository)
        {
            _gameRepository = gameRepository;
            _inviteRepository = inviteRepository;
        }

        public AcceptPickupGameResponse Handle(AcceptPickupGameRequest request)
        {
            _inviteRepository.Delete(request.InviteId);

            var game = _gameRepository.GetPickUpGameById(request.GameId);

            if (game.IsFull())
                return new AcceptPickupGameResponse { Status = ResponseCodes.GameIsFull };

            _gameRepository.AddPlayerToGame(request.GameId, request.ProfileId);


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