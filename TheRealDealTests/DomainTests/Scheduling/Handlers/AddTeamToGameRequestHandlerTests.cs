using System;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Teams;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers
{
    [TestFixture]
    public class AddTeamToGameRequestHandlerTests
    {
        private Mock<ITeamGameRepository> _gameRepository;
        private AddTeamToGameRequest _request;
        private Team _team;
        private GameWithTeams _game;

        [SetUp]
        public void SetUp()
        {
            _team = new Team();
            _game = new GameWithTeams(DateTime.Now, null, null);
        }

        [Test]
        public void CanAddATeamToAGame()
        {
            SetUpRequest();

            CreateMockRepositoriesThatReturn(_game);

            var handler = new AddTeamToGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(_request);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            _gameRepository.Verify(x => x.AddTeamToGame(_team.Id, _game.Id));
        }

        [Test]
        public void CannotAddTeamIfGameAlreadyHasMaxAmountOfTeams()
        {
            SetUpRequest();
            _game.TeamsIds.Add("123");
            _game.TeamsIds.Add("1234");

            CreateMockRepositoriesThatReturn(_game);

            var handler = new AddTeamToGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(_request);
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.GameIsFull));
        }

        private void CreateMockRepositoriesThatReturn(GameWithTeams game)
        {
            _gameRepository = new Mock<ITeamGameRepository>();
            _gameRepository.Setup(x => x.GetTeamGameById(game.Id)).Returns(game);
        }

        private void SetUpRequest()
        {
            _request = new AddTeamToGameRequest {TeamId = _team.Id, GameId = _game.Id};
        }
    }
}