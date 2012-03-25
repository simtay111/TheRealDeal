using System.Linq;
using Neo4jClient;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class BaseRepository
    {
        protected readonly GraphClient GraphClient;

        public BaseRepository(GraphClient graphClient)
        {
            GraphClient = graphClient;
        }

        public void CreateGameBaseNode()
        {
            if (GameBaseNodeExists())
                return;

            var gameBaseNode = GraphClient.Create(new SchemaNode { Type = SchemaNodeTypes.GameBase });

            var rootNode = GraphClient.RootNode;

            GraphClient.CreateRelationship(rootNode, new BaseNodeRelationship(gameBaseNode));
        }

        private bool GameBaseNodeExists()
        {
            return GraphClient.GameBaseNode().Any();
        }

        protected void CreateSportBaseNode()
        {
            if (SportBaseNodeExists())
                return;

            var sportBaseNode = GraphClient.Create(new SchemaNode { Type = SchemaNodeTypes.SportBase.ToString() });

            var rootNode = GraphClient.RootNode;

            GraphClient.CreateRelationship(rootNode, new BaseNodeRelationship(sportBaseNode));
        }

        private bool SportBaseNodeExists()
        {
            return GraphClient.SportBaseNode().Any();
        }
    }
}