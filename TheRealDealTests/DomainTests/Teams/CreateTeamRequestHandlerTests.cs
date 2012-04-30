using System.Linq;
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
            const string profileName = "profile1";


            var request = new CreateTeamRequest
                              {
                                  MaxSize = maxSize,
                                  Name = teamName,
                                  ProfileId = profileName
                              };

            var teamRepository = new Mock<ITeamRepository>();
            teamRepository.Setup(x => x.Save(It.Is<Team>(d => d.Name == teamName
                                                                      && d.MaxSize == maxSize))).Returns(true);

            var handler = new CreateTeamRequestHandle(teamRepository.Object);
            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void ThrowsExceptionWhenANameIsNotSpecified()
        {
            const int maxSize = 5;

            var request = new CreateTeamRequest
                              {
                                  MaxSize = maxSize,
                                  Name = null
                              };

            var teamRepository = new Mock<ITeamRepository>().Object;
            var handler = new CreateTeamRequestHandle(teamRepository);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NameNotSpecified));
        }

        [Test]
        public void UsesADefaultTeamSizeIfItIsSentInAsEmptyString()
        {
            const string teamName = "TeamName";
            const string profileName = "profile1";


            var request = new CreateTeamRequest()
            {
                MaxSize = 0,
                Name = teamName,
                ProfileId = profileName
            };

            var teamRepository = new Mock<ITeamRepository>();
            teamRepository.Setup(x => x.Save(It.Is<Team>(d => d.Name == teamName
                                                                      && d.MaxSize == Constants.DefaultTeamSize))).Returns(true);

            var handler = new CreateTeamRequestHandle(teamRepository.Object);
            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void MustHaveAProfileToCreateATeam()
        {
            const int maxSize = 5;
            const string teamName = "TeamName";
            const string profileName = "profile1";

            var request = new CreateTeamRequest
            {
                MaxSize = maxSize,
                Name = teamName,
                ProfileId = profileName
            };

            var teamRepository = new Mock<ITeamRepository>();
            teamRepository.Setup(x => x.Save(It.Is<Team>(d => d.Name == teamName
                                                                      && d.MaxSize == maxSize
                                                                      && d.PlayersIds.Any(y => y == profileName)))).Returns(true);

            var handler = new CreateTeamRequestHandle(teamRepository.Object);
            var response = handler.Handle(request);

            teamRepository.Verify(x => x.Save(It.Is<Team>(d => d.Name == teamName
                                                               && d.MaxSize == maxSize
                                                               && d.PlayersIds.Any(y => y == profileName))));

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void ReturnsEarlyIfProfileIdNotSpecified()
        {
            const string teamName = "TeamName";

            var request = new CreateTeamRequest
            {
                MaxSize = 5,
                Name = teamName,
            };

            var handler = new CreateTeamRequestHandle(new Mock<ITeamRepository>().Object);
            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.ProfileIdRequired));
        }

        [Test]
        public void UsesProfileAsCreatorForTeam()
        {
            const int maxSize = 5;
            const string teamName = "TeamName";
            const string profileName = "profile1";

            var request = new CreateTeamRequest
            {
                MaxSize = maxSize,
                Name = teamName,
                ProfileId = profileName
            };

            var teamRepository = new Mock<ITeamRepository>();

            var handler = new CreateTeamRequestHandle(teamRepository.Object);
            handler.Handle(request);

            teamRepository.Verify(x => x.Save(It.Is<Team>(d => d.Creator == profileName)));
        }
    }
}