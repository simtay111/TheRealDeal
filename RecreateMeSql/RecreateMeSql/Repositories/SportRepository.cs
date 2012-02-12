using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class SportRepository : ISportRepository
    {
        private readonly GraphClient _graphClient;

        public SportRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public SportRepository()
            : this(GraphClientFactory.Create()) { }

        public Sport FindByName(string name)
        {
            var sport = _graphClient.SportWithName(name).FirstOrDefault();

            return sport == null ? null : new Sport(sport.Data.Name);
        }

        public IList<string> GetNamesOfAllSports()
        {
            return _graphClient.AllSportNodes().Select(sport => sport.Data.Name).ToList();
        }

        public void CreateSport(string sportName)
        {
            var sport = new Sport(sportName);

            if (!SportBaseNodeExists())
                CreateSportBaseNode();

            var sportBaseNode = _graphClient.SportBaseNode().Single();
            var sportNode = _graphClient.Create(sport);

            _graphClient.CreateRelationship(sportBaseNode.Reference, new SportRelationship(sportNode));
        }

        private void CreateSportBaseNode()
        {
            var sportBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.SportBase.ToString() });

            var rootNode = _graphClient.RootNode;

            _graphClient.CreateRelationship(rootNode, new BaseNodeRelationship(sportBaseNode));
        }

        private bool SportBaseNodeExists()
        {
            return _graphClient.SportBaseNode().Any();
        }
    }
}