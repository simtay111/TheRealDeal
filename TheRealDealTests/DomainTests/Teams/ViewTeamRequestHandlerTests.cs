using Moq;
using NUnit.Framework;
using RecreateMe.Teams;
using RecreateMe.Teams.Handlers;

namespace TheRealDealTests.DomainTests.Teams
{
    [TestFixture]
    [Category("Unit")]
    public class ViewTeamRequestHandlerTests
    {
        [Test]
        public void CanGetTeam()
        {
            var team = new Team();

            var teamRepo = new Mock<ITeamRepository>();
            teamRepo.Setup(x => x.GetById(team.Id)).Returns(team);

            var request = new ViewTeamRequest
                              {
                                  TeamId = team.Id
                              };
            
            var handler = new ViewTeamRequestHandle(teamRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Team, Is.SameAs(team));
        }
    }
}