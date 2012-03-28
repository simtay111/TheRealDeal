using System;
using Moq;
using NUnit.Framework;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class DeletePickUpGameRequestHandlerTests
    {
        [Test]
        public void CanDeletePickUpGames()
        {
            var pickUpGame = new PickUpGame(DateTime.Now, null, null);

            var pickUpGameRepo = new Mock<IPickUpGameRepository>();

            var request = new DeletePickUpGameRequest
                              {
                                  GameId = pickUpGame.Id
                              };

            var handler = new DeletePickUpGameRequestHandler(pickUpGameRepo.Object);

            handler.Handle(request);

            pickUpGameRepo.Verify(x => x.DeleteGame(pickUpGame.Id));
        }
         
    }
}