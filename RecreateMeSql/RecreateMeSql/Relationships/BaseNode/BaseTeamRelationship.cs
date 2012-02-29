using Neo4jClient;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships.BaseNode
{
    public class BaseTeamRelationship : Relationship, IRelationshipAllowingSourceNode<SchemaNode>
    {
        public BaseTeamRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public BaseTeamRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.BaseTeam; }
        }
    }
}