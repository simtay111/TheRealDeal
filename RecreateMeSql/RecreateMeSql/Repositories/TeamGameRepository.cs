using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.GameRelationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class TeamGameRepository : ITeamGameRepository
    {
        private readonly GraphClient _graphClient;
        private readonly GameMapper _gameMapper;

        public TeamGameRepository(GraphClient graphClient, GameMapper gameMapper)
        {
            _graphClient = graphClient;
            _gameMapper = gameMapper;
        }

        public TeamGameRepository()
            : this(GraphClientFactory.Create(), new GameMapper()) { }

        public void SaveTeamGame(GameWithTeams game)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();

            var gameNode = CreateTeamGameNodeAndSaveGenericData(game);

            SaveGameWithTeamData(game, gameNode);
        }

        private void SaveGameWithTeamData(GameWithTeams game, NodeReference gameNode)
        {
            foreach (var teamId in game.TeamsIds)
            {
                CreateTeamInGameRelationship(gameNode, teamId);
            }
        }

        public GameWithTeams GetTeamGameById(string id)
        {
            var gameQuery = _graphClient.GameWithId(id);

            return _gameMapper.MapTeamGame(gameQuery);
        }

        private NodeReference<GameWithTeams> CreateTeamGameNodeAndSaveGenericData(GameWithTeams game)
        {
            var gameBaseNode = _graphClient.GameBaseNode().Single();

            var gameNode = _graphClient.Create(game);

            _graphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = _graphClient.SportWithName(game.Sport.Name).Single();

            _graphClient.CreateRelationship(sportNode.Reference, new GameToSportRelationship(gameNode));

            var locationNode = _graphClient.LocationWithName(game.Location.Name).Single();

            _graphClient.CreateRelationship(locationNode.Reference, new GameToLocationRelationship(gameNode));

            var profileNode = _graphClient.ProfileWithId(game.Creator).Single();

            _graphClient.CreateRelationship(profileNode.Reference, new CreatedByRelationship(gameNode));
            return gameNode;
        }

        public IList<GameWithTeams> GetTeamGamesForProfile(string profileId)
        {
            var gamesWithTeams = _graphClient.ProfileWithId(profileId).GamesWithTeamsForProfile().Select(x => x.Data.Id).ToList();

            return gamesWithTeams.Select(game => _gameMapper.MapTeamGame(_graphClient.GameWithId(game))).ToList();
        }

        public void AddTeamToGame(string teamid, string gameId)
        {
            var gameNode = _graphClient.GameWithId(gameId).Single();
            CreateTeamInGameRelationship(gameNode.Reference, teamid);
        }

        public void CreateGameBaseNode(string sportName)
        {
            if (!GameBaseNodeExists())
                CreateGameBaseNode();
        }

        private void CreateGameBaseNode()
        {
            var gameBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.GameBase });

            var rootNode = _graphClient.RootNode;

            _graphClient.CreateRelationship(rootNode, new BaseNodeRelationship(gameBaseNode));
        }

        private bool GameBaseNodeExists()
        {
            return _graphClient.GameBaseNode().Any();
        }

        private void CreateTeamInGameRelationship(NodeReference gameNode, string teamId)
        {
            var teamNode = _graphClient.TeamWithId(teamId).Single();
            _graphClient.CreateRelationship(teamNode.Reference, new TeamInGameRelationship(gameNode));
        }

    }
}