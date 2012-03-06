using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMe.Teams;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql
{
    public static class GremlinQueriesExtensions
    {
        public static IGremlinNodeQuery<Sport> AllSportNodes(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport).InV<Sport>();
        }

        public static IGremlinNodeQuery<SchemaNode> GameBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.GameBase);
        }

        public static IGremlinNodeQuery<RetrievedGame> GameWithId(this GraphClient gc, string id)
        {
            return gc.GameBaseNode().OutE(RelationsTypes.Game).InV<RetrievedGame>(y => y.Id == id);
        }

        public static IGremlinNodeQuery<RetrievedGame> GamesWithoutTeamsForProfile(this IGremlinNodeQuery<Profile> gc)
        {
            return gc.OutE(RelationsTypes.PlaysInGame).InV<RetrievedGame>();
        }

        public static IGremlinNodeQuery<RetrievedGame> GamesWithTeamsForProfile(this IGremlinNodeQuery<Profile> gc)
        {
            return gc.OutE(RelationsTypes.PartOfTeam).InV<Team>().OutE(RelationsTypes.TeamInGame).InV<RetrievedGame>();
        }

        public static IGremlinNodeQuery<RetrievedGame> GamesAtLocation(this GraphClient gc, string location)
        {
            return gc.LocationWithName(location).InE(RelationsTypes.GameToLocation).OutV<RetrievedGame>();
        }

        public static IGremlinNodeQuery<Profile> PlayersForGame(this IGremlinNodeQuery<RetrievedGame> gc)
        {
            return gc.InE(RelationsTypes.PlaysInGame).OutV<Profile>();
        }

        public static IGremlinNodeQuery<Team> TeamsForGame(this IGremlinNodeQuery<RetrievedGame> gc)
        {
            return gc.InE(RelationsTypes.TeamInGame).OutV<Team>();
        }

        public static IGremlinNodeQuery<SchemaNode> LocationBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase);
        }

        public static IGremlinRelationshipQuery LocationEdges(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString())
                .OutE(RelationsTypes.Location);
        }

        public static IGremlinNodeQuery<Location> LocationWithName(this GraphClient gc, string locName)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString())
                .OutE(RelationsTypes.Location).InV<Location>(y => y.Name == locName);
        }

        public static IGremlinNodeQuery<SchemaNode> SportBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase);
        }

        public static IGremlinNodeQuery<SchemaNode> TeamBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.TeamBase);
        }

        public static IGremlinRelationshipQuery SportEdges(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString())
                .OutE(RelationsTypes.Location);
        }

        public static IGremlinNodeQuery<Sport> SportWithName(this GraphClient gc, string sportName)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
               .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
               .OutE(RelationsTypes.Sport).InV<Sport>(n => n.Name == sportName);
        }

        public static IGremlinNodeQuery<Account> AccountWithId(this GraphClient gc, string accountId)
        {
            return gc.RootNode.OutE(RelationsTypes.Account)
                .InV<Account>(n => n.AccountName == accountId);
        }

        public static IGremlinNodeQuery<Team> TeamWithId(this GraphClient gc, string teamId)
        {
            return gc.TeamBaseNode().OutE(RelationsTypes.BaseTeam).InV<Team>(x => x.Id == teamId);
        }

        public static IGremlinNodeQuery<Profile> ProfilesWithTeam(this GraphClient gc, string teamId)
        {
            return gc.TeamWithId(teamId).InE(RelationsTypes.PartOfTeam).OutV<Profile>();
        }

        public static IGremlinNodeQuery<Profile> Creator(this Node<Team> teamNode )
        {
            return teamNode.InE(RelationsTypes.CreatedBy).OutV<Profile>();
        }

        public static IGremlinNodeQuery<Profile> Profiles(this IGremlinQuery gc)
        {
            return gc.OutE(RelationsTypes.HasProfile).InV<Profile>();
        }

        public static IGremlinNodeQuery<Profile> ProfileWithId(this GraphClient gc, string profileId)
        {
            return gc.RootNode.OutE(RelationsTypes.Account).InV<Account>()
                .OutE(RelationsTypes.HasProfile).InV<Profile>(n => n.ProfileId == profileId);
        }

        public static IGremlinNodeQuery<Profile> Friends(this IGremlinQuery gc)
        {
            return gc.OutE(RelationsTypes.Friend).InV<Profile>();
        }

        public static IGremlinNodeQuery<Account> Accounts(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.Account)
                 .InV<Account>();
        }
    }
}