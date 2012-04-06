using Moq;
using NUnit.Framework;
using RecreateMe.Divisions.Handlers;
using RecreateMe.Leagues;

namespace TheRealDealTests.DomainTests.Divisions.Handlers
{
    [TestFixture]
    public class AddDivisionToLeagueRequestHandlerTests
    {
        [Test]
        public void CanAddDivisionToLeague()
        {
            var leagueRepo = new Mock<ILeagueRepository>();
            var request = new AddDivisionToLeagueRequest { DivisionId = "D1", LeagueId = "League"};
            var league = new League();
            leagueRepo.Setup(x => x.GetById(request.LeagueId)).Returns(league);

            var handler = new AddDivisionToLeagueRequestHandler(leagueRepo.Object);

            var response = handler.Handle(request);

            Assert.NotNull(response);
            leagueRepo.Verify(x => x.Save(It.Is<League>(y => y.DivisionIds.Contains(request.DivisionId))));
        }
    }
}