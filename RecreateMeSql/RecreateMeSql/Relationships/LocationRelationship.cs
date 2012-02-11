using Neo4jClient;
using RecreateMe.Login;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships
{
    public class LocationRelationship : Relationship, IRelationshipAllowingSourceNode<SchemaNode>
    {
        public LocationRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public LocationRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.Location.ToString(); }
        }
    }
}