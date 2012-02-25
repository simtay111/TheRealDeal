using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Teams;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly GraphClient _graphClient;

        public TeamRepository() : this(GraphClientFactory.Create())
        {}

        public TeamRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }


        public bool Save(Team team)
        {
            if (!TeamBaseNodeExists())
                CreateTeamBaseNode();

            var teamBaseNode = _graphClient.TeamBaseNode().Single();
            var teamNode = _graphClient.Create(team);

            _graphClient.CreateRelationship(teamBaseNode.Reference, new BaseTeamRelationship(teamNode));

            return true;
        }

        private void CreateTeamBaseNode()
        {
            var teamBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.TeamBase });
            var root = _graphClient.RootNode;

            _graphClient.CreateRelationship(root, new BaseNodeRelationship(teamBaseNode));
        }

        public Team GetById(string id)
        {
            var teamNode = _graphClient.TeamWithId(id).Single();
            var team = new Team(teamNode.Data.Id) {MaxSize = teamNode.Data.MaxSize, Name = teamNode.Data.Name};

            return team;
        }

        public IList<Team> GetTeamsForProfile(string id)
        {
            return new List<Team>{TestData.CreateTeam1(), TestData.CreateTeam2()};
        }

        private bool TeamBaseNodeExists()
        {
            return _graphClient.TeamBaseNode().Any();
        }
    }
}