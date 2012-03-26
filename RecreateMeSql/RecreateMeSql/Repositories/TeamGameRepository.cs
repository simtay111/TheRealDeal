using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.GameRelationships;

namespace RecreateMeSql.Repositories
{
    public class TeamGameRepository : BaseRepository, ITeamGameRepository
    {
        private readonly GameMapper _gameMapper;

        public TeamGameRepository(GraphClient graphClient, GameMapper gameMapper)
            : base(graphClient)
        {
            _gameMapper = gameMapper;
        }

        public TeamGameRepository()
            : this(GraphClientFactory.Create(), new GameMapper()) { }


        public void SaveTeamGame(GameWithTeams game)
        {
            CreateGameBaseNode();

            var gameNode = CreateTeamGameNodeAndSaveGenericData(game);

            SaveGameWithTeamData(game, gameNode);
        }

        public GameWithTeams GetTeamGameById(string id)
        {
            var gameQuery = GraphClient.GameWithId(id);

            return _gameMapper.MapTeamGame(gameQuery);
        }

        public IList<GameWithTeams> GetTeamGamesForProfile(string profileId)
        {
            var gamesWithTeams = GraphClient.ProfileWithId(profileId).GamesWithTeamsForProfile().Select(x => x.Data.Id).ToList();

            return gamesWithTeams.Select(game => _gameMapper.MapTeamGame(GraphClient.GameWithId(game))).ToList();
        }

        public void AddTeamToGame(string teamid, string gameId)
        {
            var gameNode = GraphClient.GameWithId(gameId).Single();
            CreateTeamInGameRelationship(gameNode.Reference, teamid);
        }

        public IEnumerable<GameWithTeams> FindTeamGameByLocation(string location)
        {
            throw new System.NotImplementedException();
        }

        private NodeReference<GameWithTeams> CreateTeamGameNodeAndSaveGenericData(GameWithTeams game)
        {
            var gameBaseNode = GraphClient.GameBaseNode().Single();

            var gameNode = GraphClient.Create(game);

            GraphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = GraphClient.SportWithName(game.Sport.Name).Single();

            GraphClient.CreateRelationship(sportNode.Reference, new GameToSportRelationship(gameNode));

            var locationNode = GraphClient.LocationWithName(game.Location.Name).Single();

            GraphClient.CreateRelationship(locationNode.Reference, new GameToLocationRelationship(gameNode));

            var profileNode = GraphClient.ProfileWithId(game.Creator).Single();

            GraphClient.CreateRelationship(profileNode.Reference, new CreatedByRelationship(gameNode));
            return gameNode;
        }

        private void SaveGameWithTeamData(GameWithTeams game, NodeReference gameNode)
        {
            foreach (var teamId in game.TeamsIds)
            {
                CreateTeamInGameRelationship(gameNode, teamId);
            }
        }

        private void CreateTeamInGameRelationship(NodeReference gameNode, string teamId)
        {
            var teamNode = GraphClient.TeamWithId(teamId).Single();
            GraphClient.CreateRelationship(teamNode.Reference, new TeamInGameRelationship(gameNode));
        }

    }
}