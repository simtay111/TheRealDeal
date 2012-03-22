using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Friends.Invites;
using RecreateMe.Friends.Invites.Handlers;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDealTests.DomainTests.Friends.Invites.Handlers
{
    [TestFixture]
    public class AcceptGameInviteRequestHandlerTests
    {
        private Mock<IGameRepository> _gameRepo;
        private Mock<IInviteRepository> _inviteRepo;

        [SetUp]
        public void SetUp()
        {
            _inviteRepo = new Mock<IInviteRepository>();
            _gameRepo = new Mock<IGameRepository>();
        }

        [Test]
        public void CanAcceptGameInvite()
        {
            var request = CreateAcceptGameRequest();
            var gameWithoutTeams = new GameWithoutTeams {MaxPlayers = 1};
            _gameRepo.Setup(x => x.GetById(request.GameId)).Returns(gameWithoutTeams);
            var handler = new AcceptGameInviteRequestHandler(_gameRepo.Object, _inviteRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status == ResponseCodes.Success);
            _gameRepo.Verify(x => x.GetById(request.GameId));
            _gameRepo.Verify(x => x.AddPlayerToGame(request.GameId, request.ProfileId));
            _inviteRepo.Verify(x => x.Delete(request.InviteId));
        }

        [Test]
        public void ReturnsEarlyIfGameIsNowFull()
        {
            var request = CreateAcceptGameRequest();
            var gameWithoutTeams = new GameWithoutTeams { MaxPlayers = 0 };
            _gameRepo.Setup(x => x.GetById(request.GameId)).Returns(gameWithoutTeams);
            var handler = new AcceptGameInviteRequestHandler(_gameRepo.Object, _inviteRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status == ResponseCodes.GameIsFull);
            _gameRepo.Verify(x => x.GetById(request.GameId));
            _inviteRepo.Verify(x => x.Delete(request.InviteId), Times.Once());
            _gameRepo.Verify(x => x.AddPlayerToGame(request.GameId, request.ProfileId), Times.Never());
        }


        private static AcceptGameInviteRequest CreateAcceptGameRequest()
        {
            var request = new AcceptGameInviteRequest
                              {
                                  GameId = "123",
                                  ProfileId = "541",
                                  InviteId = "456"
                              };
            return request;
        }

        //[Test]
        //public void ReturnsIfGame
         
    }
}