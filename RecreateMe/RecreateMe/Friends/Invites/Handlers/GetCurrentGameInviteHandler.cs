using System.Collections.Generic;
using System.Linq;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Friends.Invites.Handlers
{
    public class GetCurrentGameInviteHandler : IHandler<GetCurrentGameInviteRequest, GetCurrentGameInvitesResponse>
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IPickUpGameRepository _pickUpGameRepository;

        public GetCurrentGameInviteHandler(IInviteRepository inviteRepository, IPickUpGameRepository pickUpGameRepository)
        {
            _inviteRepository = inviteRepository;
            _pickUpGameRepository = pickUpGameRepository;
        }

        public GetCurrentGameInvitesResponse Handle(GetCurrentGameInviteRequest request)
        {
            var invites = _inviteRepository.GetInvitesToProfile(request.ProfileId);

            var games = invites.Select(x => _pickUpGameRepository.GetPickUpGameById(x.EventId)).ToList();

            return new GetCurrentGameInvitesResponse { GamesWithoutTeams = games };
        }
    }

    public class GetCurrentGameInviteRequest
    {
        public string ProfileId { get; set; }
    }

    public class GetCurrentGameInvitesResponse
    {
        public List<PickUpGame> GamesWithoutTeams { get; set; }
    }
}