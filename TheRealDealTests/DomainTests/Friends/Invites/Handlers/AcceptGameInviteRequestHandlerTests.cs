using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Friends.Invites;
using RecreateMe.Friends.Invites.Handlers;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;

namespace TheRealDealTests.DomainTests.Friends.Invites.Handlers
{
    [TestFixture]
    public class AcceptGameInviteRequestHandlerTests
    {
        private Mock<IPickUpGameRepository> _gameRepo;
        private Mock<IInviteRepository> _inviteRepo;

        [SetUp]
        public void SetUp()
        {
            _inviteRepo = new Mock<IInviteRepository>();
            _gameRepo = new Mock<IPickUpGameRepository>();
        }

        [Test]
        public void CanAcceptGameInvite()
        {
            var request = CreateAcceptGameRequest();
            var gameWithoutTeams = new PickUpGame { MaxPlayers = 1 };
            _gameRepo.Setup(x => x.GetPickUpGameById(request.GameId)).Returns(gameWithoutTeams);
            var handler = new AcceptPickupGameInviteRequestHandle(_gameRepo.Object, _inviteRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status == ResponseCodes.Success);
            _gameRepo.Verify(x => x.GetPickUpGameById(request.GameId));
            _gameRepo.Verify(x => x.AddPlayerToGame(request.GameId, request.ProfileId));
            _inviteRepo.Verify(x => x.Delete(request.InviteId));
        }

        [Test]
        public void ReturnsEarlyIfGameIsNowFull()
        {
            var request = CreateAcceptGameRequest();
            var gameWithoutTeams = new PickUpGame { MaxPlayers = 0 };
            _gameRepo.Setup(x => x.GetPickUpGameById(request.GameId)).Returns(gameWithoutTeams);
            var handler = new AcceptPickupGameInviteRequestHandle(_gameRepo.Object, _inviteRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status == ResponseCodes.GameIsFull);
            _gameRepo.Verify(x => x.GetPickUpGameById(request.GameId));
            _inviteRepo.Verify(x => x.Delete(request.InviteId), Times.Once());
            _gameRepo.Verify(x => x.AddPlayerToGame(request.GameId, request.ProfileId), Times.Never());
        }


        private static AcceptPickupGameRequest CreateAcceptGameRequest()
        {
            var request = new AcceptPickupGameRequest
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