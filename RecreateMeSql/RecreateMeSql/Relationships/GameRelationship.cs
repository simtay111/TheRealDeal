using Neo4jClient;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships
{
    public class GameRelationship : Relationship, IRelationshipAllowingSourceNode<SchemaNode>
    {
        public GameRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public GameRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.Game; }
        }
    }
}