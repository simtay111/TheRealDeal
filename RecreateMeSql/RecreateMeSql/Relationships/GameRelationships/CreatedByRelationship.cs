using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Relationships.GameRelationships
{
    public class CreatedByRelationship
        : Relationship, IRelationshipAllowingSourceNode<Profile>
    {
        public CreatedByRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public CreatedByRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.CreatedBy; }
        }
    }
}