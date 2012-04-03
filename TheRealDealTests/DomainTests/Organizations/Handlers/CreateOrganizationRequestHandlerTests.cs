using Moq;
using NUnit.Framework;
using RecreateMe.Organizations;
using RecreateMe.Organizations.Handlers;

namespace TheRealDealTests.DomainTests.Organizations.Handlers
{
    [TestFixture]
    public class CreateOrganizationRequestHandlerTests
    {
        [Test]
        public void CanCreateOrganizations()
        {
            var orgRepo = new Mock<IOrganizationRepository>();

            var request = new CreateOrganizationRequest
                              {
                                  Name = "My Organization"
                              };

            var handler = new CreateOrganizationRequestHandler(orgRepo.Object);

            var response = handler.Handle(request);

            Assert.NotNull(response);

            orgRepo.Verify(x => x.Save(It.Is<Organization>(y => y.Name == request.Name)));
            orgRepo.Verify(x => x.Save(It.Is<Organization>(y => !string.IsNullOrEmpty(y.Id))));
        }
         
    }
}