using Neo4jClient;
using RecreateMe.Scheduling.Handlers.Games;

namespace RecreateMeSql.Relationships
{
    public class GameToLocationRelationship
        : Relationship, IRelationshipAllowingSourceNode<Game>
    {
        public GameToLocationRelationship(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public GameToLocationRelationship(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return RelationsTypes.GameToLocation; }
        }
    }
}