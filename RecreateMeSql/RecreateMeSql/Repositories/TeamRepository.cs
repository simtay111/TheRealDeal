using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Teams;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.Relationships.ProfileRelationships;
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

            foreach (var profileId in team.PlayersIds)
            {
                var profile = _graphClient.ProfileWithId(profileId).Single();
                _graphClient.CreateRelationship(profile.Reference, new PartOfTeamRelationship(teamNode));
            }

            return true;
        }

        public Team GetById(string id)
        {
            var teamNode = _graphClient.TeamWithId(id).Single();
            return MapTeam(teamNode);
        }

        public IList<Team> GetTeamsForProfile(string id)
        {
            var teamNodes = _graphClient.ProfileWithId(id).OutE(RelationsTypes.PartOfTeam).InV<Team>();

            return teamNodes.Select(MapTeam).ToList();
        }

        private void CreateTeamBaseNode()
        {
            var teamBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.TeamBase });
            var root = _graphClient.RootNode;

            _graphClient.CreateRelationship(root, new BaseNodeRelationship(teamBaseNode));
        }

        private Team MapTeam(Node<Team> teamNode)
        {
            var team = new Team(teamNode.Data.Id) {MaxSize = teamNode.Data.MaxSize, Name = teamNode.Data.Name};

            team.PlayersIds = _graphClient.ProfilesWithTeam(team.Id).Select(x => x.Data.ProfileId).ToList();
            return team;
        }

        private bool TeamBaseNodeExists()
        {
            return _graphClient.TeamBaseNode().Any();
        }
    }

}