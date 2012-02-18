using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql
{
    public static class GremlinQueriesExtensions
    {
        public static IGremlinNodeQuery<Sport> AllSportNodes(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport.ToString()).InV<Sport>();
        }

        public static IGremlinNodeQuery<SchemaNode> LocationBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString());
        }

         public static IGremlinRelationshipQuery LocationEdges(this GraphClient gc)
         {
             return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                 .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString())
                 .OutE(RelationsTypes.Location.ToString());
         }

         public static IGremlinNodeQuery<Location> LocationWithName(this GraphClient gc, string locName)
         {
             return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                 .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString())
                 .OutE(RelationsTypes.Location.ToString()).InV<Location>(y => y.Name == locName);
         }

        public static IGremlinNodeQuery<SchemaNode> SportBaseNode(this GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString());
        }

         public static IGremlinRelationshipQuery SportEdges(this GraphClient gc)
         {
             return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                 .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString())
                 .OutE(RelationsTypes.Location.ToString());
         }

         public static IGremlinNodeQuery<Sport> SportWithName(this GraphClient gc, string sportName)
         {
             return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport.ToString()).InV<Sport>(n => n.Name == sportName);
         }

         public static IGremlinNodeQuery<Account> AccountWithId(this GraphClient gc, string accountId)
         {
             return gc.RootNode.OutE(RelationsTypes.Account.ToString())
                 .InV<Account>(n => n.AccountName == accountId);
         }

        public static IGremlinNodeQuery<Profile> Profiles(this IGremlinQuery gc)
        {
            return gc.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>();
        }

        public static IGremlinNodeQuery<Profile> ProfileWithId(this GraphClient gc, string profileId)
        {
            return gc.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>()
                .OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>(n => n.ProfileId == profileId);
        }

        public static IGremlinNodeQuery<Profile> Friends(this IGremlinQuery gc)
        {
            return gc.OutE(RelationsTypes.Friend.ToString()).InV<Profile>();
        }
    }
}