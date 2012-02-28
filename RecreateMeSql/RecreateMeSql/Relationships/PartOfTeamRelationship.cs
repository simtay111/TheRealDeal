using Neo4jClient;
using RecreateMe.Profiles;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships
{
    public class PartOfTeamRelationship : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public PartOfTeamRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public PartOfTeamRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.PartOfTeam; }
        }
    }
}