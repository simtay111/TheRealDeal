using Moq;
using NUnit.Framework;
using RecreateMe.Divisions;
using RecreateMe.Divisions.Handlers;

namespace TheRealDealTests.DomainTests.Divisions.Handlers
{
    [TestFixture]
    public class AddTeamToDivisionRequestHandlerTests
    {
        [Test]
        public void CanAddATeamToDivision()
        {
            var request = new AddTeamToDivisionRequest {TeamId = "12345", DivisionId = "DivId"};

            var divisionRepo = new Mock<IDivisionRepository>();
            var division = new Division();
            divisionRepo.Setup(x => x.GetById(request.DivisionId)).Returns(division);

            var handler = new AddTeamToDivisionRequestHandler(divisionRepo.Object);

            var response = handler.Handle(request);

            Assert.NotNull(response);
            divisionRepo.Verify(x => x.Save(It.Is<Division>(y => y.TeamIds.Contains(request.TeamId))));
        }
         
    }
}