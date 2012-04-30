using Moq;
using NUnit.Framework;
using RecreateMe.Configuration;
using RecreateMe.ProfileSetup.Handlers;

namespace TheRealDealTests.DomainTests.ProfileSetup.Handlers
{
    [TestFixture]
    public class GetListOfConfigurableProfileOptionsHandlerTests
    {
        [Test]
        public void CanGetListOfOptions()
        {
            var configurationProvider = new Mock<IConfigurationProvider>();

            configurationProvider.Setup(x => x.GetAllConfigurableProfileOptions())
                .Returns(new []{"Option1" , "Option2"});

            var request = new GetListOfConfigurableProfileOptionsRequest();

            var handler = new GetListOfConfigurableProfileOptionsHandle(configurationProvider.Object);

            var response = handler.Handle(request);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.ListOfConfigurableOptions.Count, Is.EqualTo(2));
        }
         
    }
}