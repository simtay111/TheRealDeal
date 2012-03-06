using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class CreateGameRequestHandlerTests
    {
        private const string SoccerName = "Soccer";
        private const string LocationName = "myLoc";
        private Mock<ISportRepository> _mockSportRepo;
        private Mock<ILocationRepository> _mockLocationRepo;
        private Mock<IGameRepository> _mockGameRepo;
        private Mock<IGameFactory> _mockGameFactory;
        private Sport _sport;
        private Location _location;
        private GameWithTeams _gameWithTeams;

        [SetUp]
        public void SetUp()
        {
            _sport = new Sport { Name = SoccerName };
            _location = new Location { Name = LocationName };
            _gameWithTeams = new GameWithTeams(DateTime.Now, _sport, _location);
        }

        [Test]
        public void CanHandleRequest()
        {
            SetupMockSportLocationAndGameRepos();
            _mockGameRepo = new Mock<IGameRepository>();
            const string location = LocationName;

            var request = new CreateGameRequest
                              {
                                  DateTime = "03/03/03 12:00",
                                  Location = location,
                                  MaxPlayers = 5,
                                  MinPlayers = 3,
                                  Sport = SoccerName
                              };

            var handler = CreateHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void SavesWhichProfileCreatedTheGame()
        {
            SetupMockSportLocationAndGameRepos();
            _mockGameRepo = new Mock<IGameRepository>();
            const string location = LocationName;
            const string profile1 = "Profile1";

            var request = new CreateGameRequest
                              {
                                  DateTime = "03/03/03 12:00",
                                  Location = location,
                                  MaxPlayers = 5,
                                  MinPlayers = 3,
                                  Sport = SoccerName,
                                  Creator = profile1
                              };

            var handler = CreateHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            _mockGameRepo.Verify(x => x.Save(It.Is<Game>(y => y.Creator == profile1)));
        }

        [Test]
        public void ResponseReturnsWithGameIdOfCreatedGame()
        {
            SetupMockSportLocationAndGameRepos();
            _mockGameRepo = new Mock<IGameRepository>();
            const string location = LocationName;

            var request = new CreateGameRequest
            {
                DateTime = "03/03/03 12:00",
                Location = location,
                MaxPlayers = 5,
                MinPlayers = 3,
                Sport = SoccerName
            };

            var handler = CreateHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.That(response.GameId, Is.EqualTo(_gameWithTeams.Id));
        }

        [Test]
        public void CanCreateAGameThatIsPrivate()
        {
            var date = DateTime.Now;
            SetupMockSportLocationAndGameRepos();
            _mockGameRepo = new Mock<IGameRepository>();
            _gameWithTeams = new GameWithTeams(date, _sport, _location) {IsPrivate = true};
            var nonPrivateGame = new GameWithTeams(date, _sport, _location);
            _mockGameFactory = new Mock<IGameFactory>();
            _mockGameFactory.Setup(x => x.CreateGameWithTeams(It.IsAny<DateTime>(), It.IsAny<Sport>(), It.IsAny<Location>(), true)).Returns(
                _gameWithTeams);
            _mockGameFactory.Setup(x => x.CreateGameWithTeams(It.IsAny<DateTime>(), It.IsAny<Sport>(), It.IsAny<Location>(), false)).Returns(
                nonPrivateGame);
            _mockGameRepo.Setup(x => x.Save(It.IsAny<GameWithTeams>())).Verifiable();
            
            const string location = LocationName;

            var request = new CreateGameRequest
            {
                DateTime = date.ToLongDateString(),
                Location = location,
                MaxPlayers = 5,
                MinPlayers = 3,
                Sport = SoccerName,
                IsPrivate = true
            };

            var handler = CreateHandler();

            var response = handler.Handle(request);

            _mockGameRepo.Verify(x => x.Save(_gameWithTeams), Times.AtLeastOnce());

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
        }

        [Test]
        public void ThrowsExceptionWhenTheDateCantBeParsed()
        {
            SetupMockSportLocationAndGameRepos();

            var request = new CreateGameRequest
                              {
                                  Location = "Moo",
                                  Sport = SoccerName,
                                  DateTime = "BadDate"
                              };
            var handler = CreateHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.CouldNotParseDate));
        }

        [Test]
        public void ThrowsExceptionWhenThereIsNoDateSpecified()
        {
            SetupMockSportLocationAndGameRepos();

            var request = new CreateGameRequest
                              {
                                  Location = "Moo",
                                  Sport = SoccerName,
                                  DateTime = null
                              };
            var handler = CreateHandler();


            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.DateNotSpecified));
        }

        [Test]
        public void ThrowsExceptionWhenThereIsNoLocationSpecified()
        {
            SetupMockSportLocationAndGameRepos();

            var request = new CreateGameRequest
                              {
                                  Location = null,
                                  Sport = SoccerName,
                                  DateTime = "Sometime"
                              };
            var handler = CreateHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.LocationNotSpecified));
        }

        [Test]
        public void ThrowsExceptionWhenThereIsNoSportSpecified()
        {
            SetupMockSportLocationAndGameRepos();

            var request = new CreateGameRequest
                              {
                                  Location = "Loc",
                                  Sport = null,
                                  DateTime = "Sometime"
                              };
            var handler = CreateHandler();


            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.SportNotSpecified));
        }

        [Test]
        public void CanCreateAGameWithNoTeams()
        {
            SetupMockSportLocationAndGameRepos();
            var request = new CreateGameRequest
            {
                DateTime = "03/03/03 12:00",
                Location = LocationName,
                MaxPlayers = 5,
                MinPlayers = 3,
                Sport = SoccerName,
                HasTeams = false
            };
            var gameWithoutTeams = new GameWithoutTeams(DateTime.Parse(request.DateTime), _sport, _location);
            _mockGameFactory.Setup(
                x => x.CreateGameWithOutTeams(It.Is<DateTime>(d => d == DateTime.Parse(request.DateTime))
                    , It.Is<Sport>(d => d == _sport), It.Is<Location>(d => d == _location), It.IsAny<bool>())).Returns(
                    gameWithoutTeams);
            var theRightTypeOfGameWasReturned = false;
            _mockGameRepo.Setup(x => x.Save(It.Is<Game>(d => d == gameWithoutTeams)))
                .Callback(() => theRightTypeOfGameWasReturned = true);

            var handler = CreateHandler();

            var response = handler.Handle(request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.True(theRightTypeOfGameWasReturned);
        }

        private CreateGameRequestHandler CreateHandler()
        {
            return new CreateGameRequestHandler(_mockSportRepo.Object, _mockLocationRepo.Object,
                                                _mockGameRepo.Object, _mockGameFactory.Object);
        }

        private void SetupMockSportLocationAndGameRepos()
        {
            _mockSportRepo = new Mock<ISportRepository>();
            _mockSportRepo.Setup(x => x.FindByName(It.IsAny<string>())).Returns(_sport);
            _mockLocationRepo = new Mock<ILocationRepository>();
            _mockLocationRepo.Setup(x => x.FindByName(It.IsAny<string>())).Returns(_location);
            _mockGameRepo = new Mock<IGameRepository>();
            _mockGameRepo.Setup(x => x.Save(It.IsAny<Game>())).Returns(true);
            _mockGameFactory = new Mock<IGameFactory>();
            _mockGameFactory.Setup(
                x => x.CreateGameWithTeams(It.IsAny<DateTime>(), It.IsAny<Sport>(), It.IsAny<Location>(), It.IsAny<bool>())).Returns(
                    _gameWithTeams);
        }
    }
}