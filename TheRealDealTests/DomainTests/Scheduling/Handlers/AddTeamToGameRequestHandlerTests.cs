using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Teams;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class AddTeamToGameRequestHandlerTests
    {
        private Mock<ITeamGameRepository> _gameRepository;
        private AddTeamToGameRequest _request;
        private Team _team;
        private TeamGame _teamGame;

        [SetUp]
        public void SetUp()
        {
            _team = new Team();
            _teamGame = new TeamGame(DateTime.Now, null, null);
        }

        [Test]
        public void CanAddATeamToAGame()
        {
            SetUpRequest();

            CreateMockRepositoriesThatReturn(_teamGame);

            var handler = new AddTeamToGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(_request);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            _gameRepository.Verify(x => x.AddTeamToGame(_team.Id, _teamGame.Id));
        }

        [Test]
        public void CannotAddTeamIfGameAlreadyHasMaxAmountOfTeams()
        {
            SetUpRequest();
            _teamGame.TeamsIds.Add("123");
            _teamGame.TeamsIds.Add("1234");

            CreateMockRepositoriesThatReturn(_teamGame);

            var handler = new AddTeamToGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(_request);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.GameIsFull));
        }

        private void CreateMockRepositoriesThatReturn(TeamGame teamGame)
        {
            _gameRepository = new Mock<ITeamGameRepository>();
            _gameRepository.Setup(x => x.GetTeamGameById(teamGame.Id)).Returns(teamGame);
        }

        private void SetUpRequest()
        {
            _request = new AddTeamToGameRequest {TeamId = _team.Id, GameId = _teamGame.Id};
        }
    }
}