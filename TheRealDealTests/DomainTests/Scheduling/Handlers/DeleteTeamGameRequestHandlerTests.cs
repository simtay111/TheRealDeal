using System;
using Moq;
using NUnit.Framework;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class DeleteTeamGameRequestHandlerTests
    {
        [Test]
        public void CanDeleteTeamGames()
        {
            var teamGame = new TeamGame(DateTime.Now, null, null);

            var teamGameRepo = new Mock<ITeamGameRepository>();

            var request = new DeleteTeamGameRequest
                              {
                                  GameId = teamGame.Id
                              };

            var handler = new DeleteTeamGameRequestHandler(teamGameRepo.Object);

            handler.Handle(request);

            teamGameRepo.Verify(x => x.DeleteGame(teamGame.Id));
        }
         
    }
}