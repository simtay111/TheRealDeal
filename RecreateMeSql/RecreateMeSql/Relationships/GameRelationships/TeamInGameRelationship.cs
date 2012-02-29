using Neo4jClient;
using RecreateMe.Teams;

namespace RecreateMeSql.Relationships.GameRelationships
{
    public class TeamInGameRelationship
        : Relationship, IRelationshipAllowingSourceNode<Team>
    {
        public TeamInGameRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public TeamInGameRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.TeamInGame; }
        }
    }
}