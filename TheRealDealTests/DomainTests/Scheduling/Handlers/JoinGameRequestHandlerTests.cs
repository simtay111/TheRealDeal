using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Profiles;
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
        public void CanJoinGame()
        {
            var request = new JoinGameRequest {GameId = "1"};
            var game = new GameWithoutTeams(DateTime.Now, null, null);
            
            _mockGameRepo.Setup(x => x.GetById(request.GameId)).Returns(game);
            _mockGameRepo.Setup(x => x.Save(game)).Returns(true);

            var handler = new JoinGameRequestHandler(_mockGameRepo.Object);
            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.That(game.PlayersIds.Count, Is.EqualTo(1));
        }

        [Test]
        public void ThrowsAnExceptionWhenTryingToAddPlayersToAGameOfOnlyTeams()
        {
            var request = new JoinGameRequest { GameId = "1" };
            var game = new GameWithTeams(DateTime.Now, null, null);
            
            _mockGameRepo.Setup(x => x.GetById(request.GameId)).Returns(game);
            _mockGameRepo.Setup(x => x.Save(game)).Returns(true);

            var handler = new JoinGameRequestHandler(_mockGameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.OnlyTeamsCanJoin));
        }
    }
}