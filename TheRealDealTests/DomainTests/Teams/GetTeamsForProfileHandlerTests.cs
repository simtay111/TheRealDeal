using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Teams;
using RecreateMe.Teams.Handlers;

namespace TheRealDealTests.DomainTests.Teams
{
    [TestFixture]
    public class GetTeamsForProfileHandlerTests
    {
        [Test]
        public void CanGetTeamsForProfile()
        {
            var request = new GetTeamsForProfileRequest() {ProfileId = "123"};
            var repo = new Mock<ITeamRepository>();
            repo.Setup(x => x.GetTeamsForProfile(request.ProfileId))
                .Returns(new List<Team> {TestData.CreateTeam1()});

            var handler = new GetTeamsForProfileHandler(repo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Teams.Count, Is.EqualTo(1));
        }
    } 
}