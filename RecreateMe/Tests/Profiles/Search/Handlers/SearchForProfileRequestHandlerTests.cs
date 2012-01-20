using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Exceptions;
using RecreateMe.Exceptions.Profiling;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Search.Handlers;

namespace RecreateMe.Tests.Profiles.Search.Handlers
{
    [TestFixture]
    public class SearchForProfileRequestHandlerTests
    {
        private Mock<IProfileRepository> _mockProfileRepo;


        [Test]
        public void CanSearchForProfilesByName()
        {
            const string name = "Nancy Drew";
            var request = new SearchForProfileRequest {Name = name};
            _mockProfileRepo = CreateMockProfileRepository(name);
            var handler = new SearchForProfileRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.That(response.Profiles.Count, Is.EqualTo(3));
        }

        [Test]
        public void ThrowsAnExceptionIfNoProfilesAreFound()
        {
            const string name = "Meowsa";
            var request = new SearchForProfileRequest { Name = name };
            _mockProfileRepo = CreateMockProfileRepository("ProfileNames");
            var handler = new SearchForProfileRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NoResultsFound));
        }

        private Mock<IProfileRepository> CreateMockProfileRepository(string name)
        {
            _mockProfileRepo = new Mock<IProfileRepository>();
            _mockProfileRepo.Setup(x => x.FindAllByName(It.Is<string>(d => d == name))).Returns(
                TestData.GetListOfMockedProfiles());
            _mockProfileRepo.Setup(x => x.FindAllByName(It.Is<string>(d => d != name))).Returns(new List<Profile>());
            return _mockProfileRepo;
        }
    }
}