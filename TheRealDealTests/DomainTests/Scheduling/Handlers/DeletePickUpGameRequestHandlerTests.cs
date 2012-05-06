using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class DeletePickUpGameRequestHandlerTests
    {
        [Test]
        public void CanDeletePickUpGames()
        {
            var pickUpGame = new PickUpGame(DateTime.Now, new Sport(), new Location()) {Creator = "Creator"};

            var pickUpGameRepo = new Mock<IPickUpGameRepository>();
            pickUpGameRepo.Setup(x => x.GetPickUpGameById(pickUpGame.Id)).Returns(pickUpGame);

            var request = new DeletePickUpGameRequest
                              {
                                  GameId = pickUpGame.Id,
                                  ProfileId = "Creator"
                              };

            var handler = new DeletePickUpGameRequestHandle(pickUpGameRepo.Object);

            var response = handler.Handle(request);

            pickUpGameRepo.Verify(x => x.DeleteGame(pickUpGame.Id));
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void CanOnlyDeleteIfOwnerOfGame()
        {
            var pickUpGame = new PickUpGame(DateTime.Now, new Sport(), new Location()) {Creator = "MooCreator"};

            var pickUpGameRepo = new Mock<IPickUpGameRepository>();
            pickUpGameRepo.Setup(x => x.GetPickUpGameById(pickUpGame.Id)).Returns(pickUpGame);

            var request = new DeletePickUpGameRequest
            {
                GameId = pickUpGame.Id,
                ProfileId = "CowCreator"
            };

            var handler = new DeletePickUpGameRequestHandle(pickUpGameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.NotCreator));
            pickUpGameRepo.Verify(x => x.DeleteGame(pickUpGame.Id), Times.Never());
        }
    }
}