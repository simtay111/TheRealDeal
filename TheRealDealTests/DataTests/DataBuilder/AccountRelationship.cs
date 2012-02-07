using Neo4jClient;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class AccountRelationship : Relationship, IRelationshipAllowingSourceNode<SampleDataBuilderTests.MyType>
    {
        public AccountRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public AccountRelationship(NodeReference targetNode, SampleDataBuilderTests.MyType data) : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "Account"; }
        }
    }
}