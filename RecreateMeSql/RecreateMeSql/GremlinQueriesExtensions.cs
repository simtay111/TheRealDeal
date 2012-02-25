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

        public static IGremlinNodeQuery<SchemaNode> LocationBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode)
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString());
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
            return gc.RootNode.OutE(RelationsTypes.BaseNode).InV<SchemaNode>(n => n.Type == SchemaNodeTypes.TeamBase).OutE(RelationsTypes.BaseTeam).InV<Team>(x => x.Id == teamId);
        }

        public static IGremlinNodeQuery<Profile> Profiles(this IGremlinQuery gc)
        {
            return gc.OutE(RelationsTypes.HasProfile).InV<Profile>();
        }

        //public static IGremlinNodeQuery<Profile> ProfileIdsContaining(this IGremlinQuery gc, string name)
        //{
        //    return gc.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>(x => x.ProfileId.Contains(name));
        //}

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