using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships.ProfileRelationships
{
    public class PlaysInGameRelationship
        : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public PlaysInGameRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public PlaysInGameRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.PlaysInGame; }
        }
    }
}