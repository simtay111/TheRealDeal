using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships.TeamRelationship
{
    public class TeamCreatedByRelationship
        : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public TeamCreatedByRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public TeamCreatedByRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.TeamCreatedBy; }
        }
    }
}