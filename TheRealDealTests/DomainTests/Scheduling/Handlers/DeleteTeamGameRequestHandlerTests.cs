using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
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
            var teamGame = new TeamGame(DateTime.Now, null, null) {Creator = "Creator"};

            var teamGameRepo = new Mock<ITeamGameRepository>();
            teamGameRepo.Setup(x => x.GetTeamGameById(teamGame.Id)).Returns(teamGame);

            var request = new DeleteTeamGameRequest
                              {
                                  GameId = teamGame.Id,
                                  ProfileId = "Creator"
                              };

            var handler = new DeleteTeamGameRequestHandle(teamGameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            teamGameRepo.Verify(x => x.DeleteGame(teamGame.Id));
        }

        [Test]
        public void CanOnlyDeleteIfOwnerOfGame()
        {
            var teamGame = new TeamGame(DateTime.Now, null, null) { Creator = "MooCreator" };

            var teamGameRepo = new Mock<ITeamGameRepository>();
            teamGameRepo.Setup(x => x.GetTeamGameById(teamGame.Id)).Returns(teamGame);

            var request = new DeleteTeamGameRequest
            {
                GameId = teamGame.Id,
                ProfileId = "CowCreator"
            };

            var handler = new DeleteTeamGameRequestHandle(teamGameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NotCreator));
            teamGameRepo.Verify(x => x.DeleteGame(teamGame.Id), Times.Never());
        }
         
    }
}