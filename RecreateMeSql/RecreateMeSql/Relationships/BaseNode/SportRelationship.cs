using Neo4jClient;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships.BaseNode
{
    public class SportRelationship : Relationship, IRelationshipAllowingSourceNode<SchemaNode>
    {
        public SportRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public SportRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.Sport; }
        }
    }
}