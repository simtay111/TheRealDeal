using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships.ProfileRelationships
{
    public class PartOfTeamRelationship : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public PartOfTeamRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public PartOfTeamRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.PartOfTeam; }
        }
    }
}