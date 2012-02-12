using Neo4jClient;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Relationships
{
    public class ProfileToLocationRelationship : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public ProfileToLocationRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public ProfileToLocationRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.ProfileLocation.ToString(); }
        }
    }
}