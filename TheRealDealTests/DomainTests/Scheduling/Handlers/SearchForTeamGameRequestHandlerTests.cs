using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class SearchForTeamGameRequestHandlerTests
    {
        private Mock<ITeamGameRepository> _gameRepository;

        [Test]    
        public void CanSearchForGamesViaLocationAndSportType()
        {
            var soccer = TestData.CreateSoccerGame();
            var basketball = TestData.CreateBasketballGame();
            var location1 = TestData.CreateLocationBend();
            var location2 = TestData.CreateLocationHamsterville();

            var soccerGame1 = new TeamGame(DateTime.Now, soccer, location1);
            var soccerGame2 = new TeamGame(DateTime.Now, soccer, location2);
            var basketballGame = new TeamGame(DateTime.Now, basketball, location1);

            var listOfGames = new List<TeamGame> { soccerGame1, soccerGame2, basketballGame };

            var request = new SearchForTeamGameRequest() { Location = location1.Name, Sport = soccer.Name };

            _gameRepository = new Mock<ITeamGameRepository>();
            _gameRepository.Setup(x => x.FindTeamGameByLocation(It.Is<string>(d => d == location1.Name)))
                .Returns(listOfGames.Where(x => x.Location.Name == location1.Name).ToList());

            var handler = new SearchForTeamGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(request);

            Assert.That(response.GamesFound.Count, Is.EqualTo(1));
            Assert.That(response.GamesFound[0].Location.Name, Is.EqualTo(location1.Name));
            Assert.That(response.GamesFound[0].Sport.Name, Is.EqualTo(soccer.Name));
        }

        [Test]
        public void ThrowsExceptionWhenLocationIsNotSpecified()
        {

            var request = new SearchForTeamGameRequest { Location = null, Sport = "Soccer" };

            _gameRepository = new Mock<ITeamGameRepository>();

            var handler = new SearchForTeamGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.LocationNotSpecified));
        }

        [Test]
        public void IfNoSportWasSpecifiedItDoesNotFilter()
        {
            var soccer = TestData.CreateSoccerGame();
            var location1 = TestData.CreateLocationBend();

            var soccerGame1 = new TeamGame(DateTime.Now, soccer, location1);
            var listOfGames = new List<TeamGame> { soccerGame1 };

            var request = new SearchForTeamGameRequest { Location = location1.Name, Sport = string.Empty };

            _gameRepository = new Mock<ITeamGameRepository>();
            _gameRepository.Setup(x => x.FindTeamGameByLocation(It.Is<string>(d => d == location1.Name)))
                .Returns(listOfGames.Where(x => x.Location.Name == location1.Name).ToList());

            var handler = new SearchForTeamGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(request);

            Assert.That(response.GamesFound.Count, Is.EqualTo(1));
            Assert.That(response.GamesFound[0].Location.Name, Is.EqualTo(location1.Name));
            Assert.That(response.GamesFound[0].Sport.Name, Is.EqualTo(soccer.Name));
        }        
    }
}