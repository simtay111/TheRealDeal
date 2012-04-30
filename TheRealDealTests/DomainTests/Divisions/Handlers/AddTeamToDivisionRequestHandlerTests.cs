using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Divisions;
using RecreateMe.Divisions.Handlers;

namespace TheRealDealTests.DomainTests.Divisions.Handlers
{
    [TestFixture]
    public class AddTeamToDivisionRequestHandlerTests
    {
        private Mock<IDivisionRepository> _divisionRepo;
        private AddTeamToDivisionRequestHandle _handle;

        [SetUp]
        public void SetUp()
        {
            _divisionRepo = new Mock<IDivisionRepository>();
            _handle = new AddTeamToDivisionRequestHandle(_divisionRepo.Object);
        }

        [Test]
        public void CanAddATeamToDivision()
        {
            var request = new AddTeamToDivisionRequest {TeamId = "12345", DivisionId = "DivId"};
            var division = new Division();
            _divisionRepo.Setup(x => x.GetById(request.DivisionId)).Returns(division);

            var response = _handle.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            _divisionRepo.Verify(x => x.AddTeamToDivision(division, request.TeamId));
        }

        [Test]
        public void CannotAddATeamToDivisionTwice()
        {
            var request = new AddTeamToDivisionRequest { TeamId = "12345", DivisionId = "DivId" };
            var division = new Division();
            division.TeamIds.Add(request.TeamId);
            _divisionRepo.Setup(x => x.GetById(request.DivisionId)).Returns(division);

            var response = _handle.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.DuplicateEntryFound));
            _divisionRepo.Verify(x => x.AddTeamToDivision(division, request.TeamId), Times.Never());
        }
         
    }
}