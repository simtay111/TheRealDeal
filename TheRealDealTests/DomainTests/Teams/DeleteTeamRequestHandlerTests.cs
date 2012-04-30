using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Teams;
using RecreateMe.Teams.Handlers;

namespace TheRealDealTests.DomainTests.Teams
{
    [TestFixture]
    public class DeleteTeamRequestHandlerTests
    {
        [Test]
        public void CanDeleteATeam()
        {
            var teamRepo = new Mock<ITeamRepository>();

            var request = new DeleteTeamRequest()
                              {
                                  TeamId = "123",
                                  ProfileId = "ProfileId"
                              };
            var team = new Team(request.TeamId) { Creator = request.ProfileId };
            teamRepo.Setup(x => x.GetById(request.TeamId)).Returns(team);

            var handler = new DeleteTeamRequestHandle(teamRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            teamRepo.Verify(x => x.DeleteTeam(request.TeamId));
        }

        [Test]
        public void CanOnlyDeleteIfOwner()
        {
            var teamRepo = new Mock<ITeamRepository>();

            var request = new DeleteTeamRequest
                              {
                TeamId = "123",
                ProfileId = "ProfileId"
            };
            var team = new Team(request.TeamId) {Creator = "NotCreator"};
            teamRepo.Setup(x => x.GetById(request.TeamId)).Returns(team);

            var handler = new DeleteTeamRequestHandle(teamRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NotCreator));
        }
    }
}