using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class GetGamesForProfileRequestHandlerTests
    {
        [Test]
        public void CanGetGamesForProfile()
        {
            var profileId = "1234";
            var game = new GameWithTeams(DateTimeOffset.Now, null, null);
            var gameREpo = new Mock<IGameRepository>();
            gameREpo.Setup(x => x.GetForProfile(profileId)).Returns(new List<Game>{game});

            var request = new GetGamesForProfileRequest
                              {
                                  ProfileId = profileId
                              };

            var handler = new GetGamesForProfileRequestHandler(gameREpo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Games[0], Is.SameAs(game));
        }
         
    }
}