using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Friends.Invites.Handlers
{
    public class AcceptGameInviteRequestHandler : IHandler<AcceptGameInviteRequest, AcceptGameInviteResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IInviteRepository _inviteRepository;

        public AcceptGameInviteRequestHandler(IGameRepository gameRepository, IInviteRepository inviteRepository)
        {
            _gameRepository = gameRepository;
            _inviteRepository = inviteRepository;
        }

        public AcceptGameInviteResponse Handle(AcceptGameInviteRequest request)
        {
            _inviteRepository.Delete(request.InviteId);

            var game = _gameRepository.GetById(request.GameId) as GameWithoutTeams;

            if (game.IsFull())
                return new AcceptGameInviteResponse {Status = ResponseCodes.GameIsFull};

            _gameRepository.AddPlayerToGame(request.GameId, request.ProfileId);


            return new AcceptGameInviteResponse {Status = ResponseCodes.Success};
        }
    }   
    
    public class AcceptGameInviteRequest{
        public string ProfileId { get; set; }

        public string GameId { get; set; }

        public string InviteId { get; set; }
    }
    
    public class AcceptGameInviteResponse
    {
        public ResponseCodes Status { get; set; }
    }
}