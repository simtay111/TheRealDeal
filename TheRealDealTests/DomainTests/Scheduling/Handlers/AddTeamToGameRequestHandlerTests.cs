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
        private Mock<IGameRepository> _gameRepository;
        private AddTeamToGameRequest _request;
        private Team _team;
        private Game _game;

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

            CreateMockRepositoriesThatReturn(_game, _team);

            var handler = new AddTeamToGameRequestHandler(_gameRepository.Object);

             var response = handler.Handle(_request);
             Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
         }

        [Test]
        public void ThrowsAnExceptionIfTheGameCantHaveTeams()
        {
            SetUpRequest();
            _game = new GameWithoutTeams(DateTime.Now, null, null);
           CreateMockRepositoriesThatReturn(_game, _team);
            var handler = new AddTeamToGameRequestHandler(_gameRepository.Object);

            var response = handler.Handle(_request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.CannotHaveTeams));
        }

        private void CreateMockRepositoriesThatReturn(Game game, Team team)
        {
            _gameRepository = new Mock<IGameRepository>();
            _gameRepository.Setup(x => x.Save(It.Is<GameWithTeams>(d => d.TeamsIds[0] == team.Id))).Returns(true);
            _gameRepository.Setup(x => x.GetById(It.Is<string>(d => d == game.Id))).Returns(game);
        }

        private void SetUpRequest()
        {
            _request = new AddTeamToGameRequest {TeamId = _team.Id, GameId = _game.Id};
        }
    }
}