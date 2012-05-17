using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Configuration;
using RecreateMe.GameMaintenance;
using RecreateMe.Locales;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.GameMaintenance
{
    [TestFixture]
    public class OldGameRemoverTests
    {
        [Test]
        public void RemovesGamesThatOld()
        {
            var game1 = new PickUpGame(DateTime.Now, new Sport(), new Location());
            var game2 = new PickUpGame(DateTime.Now, new Sport(), new Location());
            var game3 = new PickUpGame(DateTime.Now, new Sport(), new Location());
            var games = new List<PickUpGame> {game1, game2, game3};
            var gameRepo = new Mock<IPickUpGameRepository>();
            gameRepo.Setup(x => x.GetGamesWithinDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(games);

            var gameCleaner = new OldGameRemover(gameRepo.Object);

            gameCleaner.CleanForPastMinutes(15);

            gameRepo.Verify(x => x.DeleteGame(game1.Id));
            gameRepo.Verify(x => x.DeleteGame(game2.Id));
            gameRepo.Verify(x => x.DeleteGame(game3.Id));
        }
    }
}