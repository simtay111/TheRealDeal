using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Scheduling.Handlers.Games;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class SearchForGameRequestHandlerTests
    {
        private Mock<IGameRepository> _gameRepository;

        [Test]    
        public void CanSearchForGamesViaLocationAndSportType()
        {
            var soccer = TestData.CreateSoccerGame();
            var basketball = TestData.CreateBasketballGame();
            var location1 = TestData.CreateLocationBend();
            var location2 = TestData.CreateLocationHamsterville();

            var soccerGame1 = new GameWithTeams(DateTime.Now, soccer, location1);
            var soccerGame2 = new GameWithTeams(DateTime.Now, soccer, location2);
            var basketballGame = new GameWithTeams(DateTime.Now, basketball, location1);
            var listOfGames = new List<Game> {soccerGame1, soccerGame2, basketballGame};

            var request = new SearchForGameRequest {Location = location1.Name, Sport = soccer.Name};

            _gameRepository = new Mock<IGameRepository>();
            _gameRepository.Setup(x => x.FindByLocation(It.Is<string>(d => d == location1.Name)))
                .Returns(listOfGames.Where(x => x.Location.Name == location1.Name).ToList());

            var handler = new SearchForGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(request);

            Assert.That(response.GamesFound.Count, Is.EqualTo(1));
            Assert.That(response.GamesFound[0].Location.Name, Is.EqualTo(location1.Name));
            Assert.That(response.GamesFound[0].Sport.Name, Is.EqualTo(soccer.Name));
        }

        [Test]
        public void ThrowsExceptionWhenLocationIsNotSpecified()
        {

            var request = new SearchForGameRequest { Location = null, Sport = "Soccer" };

            _gameRepository = new Mock<IGameRepository>();

            var handler = new SearchForGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.LocationNotSpecified));
        }
    }
}