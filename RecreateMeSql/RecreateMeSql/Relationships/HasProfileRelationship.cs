using Neo4jClient;
using RecreateMe.Login;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships
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
            get { return RelationsTypes.HasProfile.ToString(); }
        }
    }
}