using Neo4jClient;

namespace RecreateMeSql.Relationships
{
    public class BaseNodeRelationship : Relationship, IRelationshipAllowingSourceNode<RootNode>
    {
        public BaseNodeRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public BaseNodeRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.BaseNode; }
        }
    }
}