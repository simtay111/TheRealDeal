using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
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


        public void SaveTeamGame(TeamGame teamGame)
        {
            CreateGameBaseNode();

            var gameNode = CreateTeamGameNodeAndSaveGenericData(teamGame);

            SaveGameWithTeamData(teamGame, gameNode);
        }

        public TeamGame GetTeamGameById(string id)
        {
            var gameQuery = GraphClient.GameWithId(id);

            return _gameMapper.MapTeamGame(gameQuery);
        }

        public IList<TeamGame> GetTeamGamesForProfile(string profileId)
        {
            var gamesWithTeams = GraphClient.ProfileWithId(profileId).TeamGamesForProfile().Select(x => x.Data.Id).ToList();

            return gamesWithTeams.Select(game => _gameMapper.MapTeamGame(GraphClient.GameWithId(game))).ToList();
        }

        public void AddTeamToGame(string teamid, string gameId)
        {
            var gameNode = GraphClient.GameWithId(gameId).Single();
            CreateTeamInGameRelationship(gameNode.Reference, teamid);
        }

        public IEnumerable<TeamGame> FindTeamGameByLocation(string location)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteGame(string id)
        {
            var gameNodeReference = GraphClient.GameWithId(id).SingleOrDefault();

            if (gameNodeReference == null)
                return;

            GraphClient.Delete(gameNodeReference.Reference, DeleteMode.NodeAndRelationships);
        }

        public IList<TeamGame> GetAllGamesBeforeDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        private NodeReference<TeamGame> CreateTeamGameNodeAndSaveGenericData(TeamGame teamGame)
        {
            var gameBaseNode = GraphClient.GameBaseNode().Single();

            var gameNode = GraphClient.Create(teamGame);

            GraphClient.CreateRelationship(gameBaseNode.Reference, new GameRelationship(gameNode));

            var sportNode = GraphClient.SportWithName(teamGame.Sport.Name).Single();

            GraphClient.CreateRelationship(sportNode.Reference, new GameToSportRelationship(gameNode));

            var locationNode = GraphClient.LocationWithName(teamGame.Location.Name).Single();

            GraphClient.CreateRelationship(locationNode.Reference, new GameToLocationRelationship(gameNode));

            var profileNode = GraphClient.ProfileWithId(teamGame.Creator).Single();

            GraphClient.CreateRelationship(profileNode.Reference, new CreatedByRelationship(gameNode));
            return gameNode;
        }

        private void SaveGameWithTeamData(TeamGame teamGame, NodeReference gameNode)
        {
            foreach (var teamId in teamGame.TeamsIds)
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