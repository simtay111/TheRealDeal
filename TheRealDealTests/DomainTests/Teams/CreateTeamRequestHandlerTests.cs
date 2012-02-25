using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Teams;
using RecreateMe.Teams.Handlers;

namespace TheRealDealTests.DomainTests.Teams
{
    [TestFixture]
    public class CreateTeamRequestHandlerTests
    {
        [Test]
        public void CanCreateATeam()
        {
            const int maxSize = 5;
            const string teamName = "TeamName";

            var request = new CreateTeamRequest
                              {
                                  MaxSize = maxSize,
                                  Name = teamName
                              };

            var teamRepository = new Mock<ITeamRepository>();
            teamRepository.Setup(x => x.Save(It.Is<Team>(d => d.Name == teamName
                                                                      && d.MaxSize == maxSize))).Returns(true);

            var handler = new CreateTeamRequestHandler(teamRepository.Object);
            var response = handler.Handle(request);

            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void ThrowsExceptionWhenANameIsNotSpecified()
        {
            const int maxSize = 5;

            var request = new CreateTeamRequest()
            {
                MaxSize = maxSize,
                Name = null
            };

            var teamRepository = new Mock<ITeamRepository>().Object;
            var handler = new CreateTeamRequestHandler(teamRepository);

            var response = handler.Handle(request);

            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.NameNotSpecified));
        }

        [Test]
        public void UsesADefaultTeamSizeIfItIsSentInAsEmptyString()
        {
            const string teamName = "TeamName";

            var request = new CreateTeamRequest()
            {
                MaxSize = 0,
                Name = teamName
            };

            var teamRepository = new Mock<ITeamRepository>();
            teamRepository.Setup(x => x.Save(It.Is<Team>(d => d.Name == teamName
                                                                      && d.MaxSize == Constants.DefaultTeamSize))).Returns(true);

            var handler = new CreateTeamRequestHandler(teamRepository.Object);
            var response = handler.Handle(request);

            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.Success));
        }
    }
}