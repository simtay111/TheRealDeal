using Moq;
using NUnit.Framework;
using RecreateMe.Leagues;
using RecreateMe.Leagues.Handlers;

namespace TheRealDealTests.DomainTests.Leagues
{
    [TestFixture]
    public class CreateLeagueRequestHandlerTests
    {
        [Test]
        public void CanCreateLeague()
        {
            var leagueRepo = new Mock<ILeagueRepository>();

            var request = new CreateLeagueRequest
                              {
                                  Name = "Leage1Name"
                              };

            var handler = new CreateLeagueRequestHandle(leagueRepo.Object);

            var response = handler.Handle(request);

            Assert.NotNull(response);
            leagueRepo.Verify(x => x.Save(It.Is<League>(y => y.Name == request.Name)));
            leagueRepo.Verify(x => x.Save(It.Is<League>(y => !string.IsNullOrEmpty(y.Id))));
        }
    }
}