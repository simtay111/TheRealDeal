using Neo4jClient;
using RecreateMe.Locales;

namespace RecreateMeSql.Relationships.GameRelationships
{
    public class GameToLocationRelationship
        : Relationship, IRelationshipAllowingSourceNode<Location>
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