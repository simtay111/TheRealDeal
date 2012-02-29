using Neo4jClient;

namespace RecreateMeSql.Relationships.AccountRelationships
{
    public class AccountRelationship : Relationship, IRelationshipAllowingSourceNode<RootNode>
    {
        public AccountRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public AccountRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.Account; }
        }
    }
}