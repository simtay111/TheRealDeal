using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Divisions.Handlers;
using RecreateMe.Leagues;

namespace TheRealDealTests.DomainTests.Divisions.Handlers
{
    [TestFixture]
    public class AddDivisionToLeagueRequestHandlerTests
    {
        private Mock<ILeagueRepository> _leagueRepo;
        private AddDivisionToLeagueRequestHandle _handle;

        [SetUp]
        public void SetUp()
        {
            _leagueRepo = new Mock<ILeagueRepository>();
            _handle = new AddDivisionToLeagueRequestHandle(_leagueRepo.Object);
        }

        [Test]
        public void CanAddDivisionToLeague()
        {
            var request = new AddDivisionToLeagueRequest { DivisionId = "D1", LeagueId = "League" };
            var league = new League();
            _leagueRepo.Setup(x => x.GetById(request.LeagueId)).Returns(league);

            var response = _handle.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            _leagueRepo.Verify(x => x.AddDivisionToLeague(league,
                request.DivisionId));
        }

        [Test]
        public void CanNotAddADivisionTwice()
        {
            var request = new AddDivisionToLeagueRequest { DivisionId = "D1", LeagueId = "League" };
            var league = new League();
            league.DivisionIds.Add(request.DivisionId);
            _leagueRepo.Setup(x => x.GetById(request.LeagueId)).Returns(league);

            var response = _handle.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.AlreadyInLeague));
            _leagueRepo.Verify(x => x.AddDivisionToLeague(league,
                request.DivisionId), Times.Never());
        }
    }
}