using System;
using System.Collections.Generic;
using System.Linq;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMe.Friends.Invites.Handlers
{
    public class GetCurrentGameInviteHandler : IHandler<GetCurrentGameInviteRequest, GetCurrentGameInvitesResponse>
    {
        private readonly IInviteRepository _inviteRepository;
        private readonly IGameRepository _gameRepository;

        public GetCurrentGameInviteHandler(IInviteRepository inviteRepository, IGameRepository gameRepository)
        {
            _inviteRepository = inviteRepository;
            _gameRepository = gameRepository;
        }

        public GetCurrentGameInvitesResponse Handle(GetCurrentGameInviteRequest request)
        {
            var invites = _inviteRepository.GetInvitesToProfile(request.ProfileId);

            var games = invites.Select(x => _gameRepository.GetPickUpGameById(x.EventId)).ToList();

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