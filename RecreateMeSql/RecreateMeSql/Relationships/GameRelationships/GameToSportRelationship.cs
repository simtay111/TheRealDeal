using Neo4jClient;
using RecreateMe.Sports;

namespace RecreateMeSql.Relationships.GameRelationships
{
    public class GameToSportRelationship : Relationship, IRelationshipAllowingSourceNode<Sport>
    {
        public GameToSportRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public GameToSportRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.GameToSport; }
        }
    }
}