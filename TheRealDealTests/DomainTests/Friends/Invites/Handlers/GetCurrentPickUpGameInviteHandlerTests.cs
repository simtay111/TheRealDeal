using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Friends.Invites;
using RecreateMe.Friends.Invites.Handlers;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDealTests.DomainTests.Friends.Invites.Handlers
{
    [TestFixture]
    public class GetCurrentPickUpGameInviteHandlerTests
    {
        [Test]
        public void CanGetCurrentInvitesToOtherGames()
        {
            var inviteRepo = new Mock<IInviteRepository>();
            var gameRepo = new Mock<IGameRepository>();

            var request = new GetCurrentGameInviteRequest
                              {
                                  ProfileId = "123"
                              };
            var invite = new Invite { EventId = "1234" };
            inviteRepo.Setup(x => x.GetInvitesToProfile(request.ProfileId)).Returns(new List<Invite> { invite });
            var gameWithoutTeams = new PickUpGame();
            gameRepo.Setup(x => x.GetPickUpGameById(invite.EventId)).Returns(gameWithoutTeams);

            var handler = new GetCurrentGameInviteHandler(inviteRepo.Object, gameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.GamesWithoutTeams, Is.Not.Empty);
            Assert.That(response.GamesWithoutTeams[0], Is.SameAs(gameWithoutTeams));
        }
         
    }
}