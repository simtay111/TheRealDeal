using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class JoinGameRequestHandlerTests
    {
        private Mock<IGameRepository> _mockGameRepo;

        [SetUp]
        public void SetUp()
        {
            _mockGameRepo = new Mock<IGameRepository>();
        }

        [Test]
        public void CannotJoinGameAlreadyAPartOf()
        {
            var request = new JoinGameRequest { GameId = "1", ProfileId = "123" };
            var game = new PickUpGame(DateTime.Now, null, null);
            game.PlayersIds.Add("123");

            _mockGameRepo.Setup(x => x.GetPickUpGameById(request.GameId)).Returns(game);

            var handler = new JoinGameRequestHandler(_mockGameRepo.Object);
            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.AlreadyInGame));
        }

        [Test]
        public void CannotJoinGameIfFull()
        {
            var request = new JoinGameRequest { GameId = "1", ProfileId = "123" };
            var game = new PickUpGame(DateTime.Now, null, null) { MaxPlayers = 0 };

            _mockGameRepo.Setup(x => x.GetPickUpGameById(request.GameId)).Returns(game);

            var handler = new JoinGameRequestHandler(_mockGameRepo.Object);
            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.GameIsFull));
        }

        [Test]
        public void CanJoinGame()
        {
            var request = new JoinGameRequest { GameId = "1", ProfileId = "123" };
            var game = new PickUpGame(DateTime.Now, null, null);

            _mockGameRepo.Setup(x => x.GetPickUpGameById(request.GameId)).Returns(game);

            var handler = new JoinGameRequestHandler(_mockGameRepo.Object);
            var response = handler.Handle(request);

            _mockGameRepo.Verify(x => x.AddPlayerToGame(game.Id, request.ProfileId));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }
    }
}