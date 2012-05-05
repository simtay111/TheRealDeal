using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class GetGamesForProfileRequestHandlerTests
    {
        [Test]
        public void CanGetGamesForProfile()
        {
            var profileId = "1234";
            var pickUpGame = new PickUpGame(DateTime.Now, null, null);
            var gameWithTeams = new TeamGame(DateTime.Now, null, null);
            var gameRepo = new Mock<IPickUpGameRepository>();
            var teamRepo = new Mock<ITeamGameRepository>();
            gameRepo.Setup(x => x.GetPickupGamesForProfile(profileId)).Returns(new List<PickUpGame> { pickUpGame });
            teamRepo.Setup(x => x.GetTeamGamesForProfile(profileId)).Returns(new List<TeamGame> { gameWithTeams });

            var request = new GetGamesForProfileRequest
                              {
                                  ProfileId = profileId
                              };

            var handler = new GetGamesForProfileRequestHandle(gameRepo.Object, teamRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.TeamGames[0], Is.SameAs(gameWithTeams));
            Assert.That(response.PickupGames[0], Is.SameAs(pickUpGame));
        }
         
    }
}