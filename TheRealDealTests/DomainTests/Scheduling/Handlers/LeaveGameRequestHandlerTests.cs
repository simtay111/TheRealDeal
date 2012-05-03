using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class LeaveGameRequestHandlerTests
    {
        private Mock<IPickUpGameRepository> _gameRepo;

        [SetUp]
        public void SetUp()
        {
            _gameRepo = new Mock<IPickUpGameRepository>();
        }

        [Test]
        public void CanDoSomething()
        {
            var request = new LeaveGameRequest {ProfileId = "Prof1", GameId = "123"};

            var handler = new LeaveGameRequestHandler(_gameRepo.Object);

            var response = handler.Handle(request);

            _gameRepo.Verify(x => x.RemovePlayerFromGame(request.ProfileId, request.GameId));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }
    }
}
