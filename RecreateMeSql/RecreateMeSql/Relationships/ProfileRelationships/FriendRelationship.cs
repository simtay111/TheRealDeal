using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships.ProfileRelationships
{
    public class FriendRelationship : Relationship, IRelationshipAllowingSourceNode<Profile>, IRelationshipAllowingTargetNode<Profile>
    {
        public FriendRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public FriendRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.Friend; }
        }
    }
}