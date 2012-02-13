using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships
{
    public class ProfileToSportRelationship : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public ProfileToSportRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public ProfileToSportRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.ProfileSport.ToString(); }
        }
    }
}