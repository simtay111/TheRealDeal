using Neo4jClient;
using RecreateMe.Login;

namespace RecreateMeSql.Relationships.AccountRelationships
{
    public class HasProfileRelationship : Relationship, IRelationshipAllowingSourceNode<Account>
    {
        public HasProfileRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public HasProfileRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.HasProfile; }
        }
    }
}