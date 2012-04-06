using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Leagues;
using RecreateMe.Organizations;
using RecreateMe.Organizations.Handlers;

namespace TheRealDealTests.DomainTests.Leagues
{
    [TestFixture]
    public class AddLeagueToOrganizationRequestHandlerTests
    {
        [Test]
        public void CanAddLeagueToOrganziations()
        {
            var organization = new Organization();
            var orgRepo = new Mock<IOrganizationRepository>();

            var request = new AddLeagueToOrganizationRequest
                              {
                                  OrganizationId = "123",
                                  LeagueId = "LeagueId"
                              };

            orgRepo.Setup(x => x.GetById(request.OrganizationId)).Returns(organization);

            var handler = new AddLeagueToOrganizationRequestHandler(orgRepo.Object);

            var response = handler.Handle(request);

            Assert.NotNull(response);
            Assert.That(organization.LeagueIds.Count, Is.EqualTo(1));
            Assert.That(organization.LeagueIds[0], Is.EqualTo(request.LeagueId));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            orgRepo.Verify(x => x.Save(organization));
        }

        [Test]
        public void ReturnsEarlyWithMessageIfLeagueIsAlreadyInOrganization()
        {
            var organization = new Organization();
            const string leagueId = "LeagueId";
            organization.LeagueIds.Add(leagueId);
            var orgRepo = new Mock<IOrganizationRepository>();

            var request = new AddLeagueToOrganizationRequest
            {
                OrganizationId = "123",
                LeagueId = leagueId
            };

            orgRepo.Setup(x => x.GetById(request.OrganizationId)).Returns(organization);

            var handler = new AddLeagueToOrganizationRequestHandler(orgRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.AlreadyInLeague));
            Assert.That(organization.LeagueIds.Count, Is.EqualTo(1));
            orgRepo.Verify(x => x.Save(organization), Times.Never());
        }
    }
}