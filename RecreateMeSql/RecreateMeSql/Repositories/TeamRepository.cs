using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Teams;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.GameRelationships;
using RecreateMeSql.Relationships.ProfileRelationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class TeamRepository : BaseRepository, ITeamRepository
    {
        public TeamRepository() : this(GraphClientFactory.Create())
        {}

        public TeamRepository(GraphClient graphClient) : base (graphClient)
        {
        }


        public bool Save(Team team)
        {
            if (!TeamBaseNodeExists())
                CreateTeamBaseNode();

            var teamBaseNode = GraphClient.TeamBaseNode().Single();
            var teamNode = GraphClient.Create(team);

            GraphClient.CreateRelationship(teamBaseNode.Reference, new BaseTeamRelationship(teamNode));

            foreach (var profileId in team.PlayersIds)
            {
                var profile = GraphClient.ProfileWithId(profileId).Single();
                if (profileId == team.Creator)
                    GraphClient.CreateRelationship(profile.Reference, new CreatedByRelationship(teamNode));
                GraphClient.CreateRelationship(profile.Reference, new PartOfTeamRelationship(teamNode));
            }

            return true;
        }

        public Team GetById(string id)
        {
            var teamNode = GraphClient.TeamWithId(id).Single();
            return MapTeam(teamNode);
        }

        public IList<Team> GetTeamsForProfile(string id)
        {
            var teamNodes = GraphClient.ProfileWithId(id).OutE(RelationsTypes.PartOfTeam).InV<Team>();

            return teamNodes.Select(MapTeam).ToList();
        }

        private void CreateTeamBaseNode()
        {
            var teamBaseNode = GraphClient.Create(new SchemaNode { Type = SchemaNodeTypes.TeamBase });
            var root = GraphClient.RootNode;

            GraphClient.CreateRelationship(root, new BaseNodeRelationship(teamBaseNode));
        }

        private Team MapTeam(Node<Team> teamNode)
        {
            var team = new Team(teamNode.Data.Id) {MaxSize = teamNode.Data.MaxSize, Name = teamNode.Data.Name};

            team.PlayersIds = GraphClient.ProfilesWithTeam(team.Id).Select(x => x.Data.ProfileId).ToList();
            team.Creator = teamNode.Creator().Single().Data.ProfileId;
            return team;
        }

        private bool TeamBaseNodeExists()
        {
            return GraphClient.TeamBaseNode().Any();
        }
    }

}