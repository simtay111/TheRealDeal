using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
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

            var teamGameRepo = new Mock<ITeamGameRepository>();
            var dateTime = new DateTime(2000,12,12);
            var teamGame1 = new TeamGame(dateTime.AddHours(-1), new Sport(), new Location());
            var teamGame2 = new TeamGame(dateTime.AddHours(-2), new Sport(), new Location());

            teamGameRepo.Setup(x => x.GetAllGamesBeforeDate(It.IsAny<DateTime>())).Returns(new List<TeamGame> {teamGame1, teamGame2});

            var gameCleaner = new OldGameRemover(teamGameRepo.Object);

            gameCleaner.Clean();

            teamGameRepo.Verify(x => x.DeleteGame(teamGame1.Id), Times.Once());
            teamGameRepo.Verify(x => x.DeleteGame(teamGame2.Id), Times.Once());
        }
    }
}