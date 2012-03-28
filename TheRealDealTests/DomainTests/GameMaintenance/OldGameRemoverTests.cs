using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.GameMaintenance;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;

namespace TheRealDealTests.DomainTests.GameMaintenance
{
    [TestFixture]
    public class OldGameRemoverTests
    {
        [Test]
        public void RemovesGamesThatOld()
        {

            var teamGameRepo = new Mock<ITeamGameRepository>();
            var dateTime = new DateTime(2000,12,12);
            var teamGame1 = new TeamGame(new DateTimeOffset(dateTime.AddHours(-1)), null, null);
            var teamGame2 = new TeamGame(new DateTimeOffset(dateTime.AddHours(-2)), null, null);

            teamGameRepo.Setup(x => x.GetAllGamesBeforeDate(It.IsAny<DateTime>())).Returns(new List<TeamGame> {teamGame1, teamGame2});

            var gameCleaner = new OldGameRemover(teamGameRepo.Object);

            gameCleaner.Clean();

            teamGameRepo.Verify(x => x.DeleteGame(teamGame1.Id), Times.Once());
            teamGameRepo.Verify(x => x.DeleteGame(teamGame2.Id), Times.Once());
        }
    }
}