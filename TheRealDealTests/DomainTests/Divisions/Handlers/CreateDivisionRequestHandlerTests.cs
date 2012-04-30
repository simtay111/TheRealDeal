using Moq;
using NUnit.Framework;
using RecreateMe.Divisions;
using RecreateMe.Divisions.Handlers;

namespace TheRealDealTests.DomainTests.Divisions.Handlers
{
    [TestFixture]
    public class CreateDivisionRequestHandlerTests
    {
        [Test]
        public void CanCreateDivisions()
        {
            var divisionRepo = new Mock<IDivisionRepository>();

            var request = new CreateDivisionRequest {Name = "D1"};

            var handler = new CreateDivisionRequestHandle(divisionRepo.Object);

            var response = handler.Handle(request);

            Assert.NotNull(response);
            divisionRepo.Verify(x => x.Save(It.Is<Division>(y => y.Name == request.Name)));
        }
    }
}