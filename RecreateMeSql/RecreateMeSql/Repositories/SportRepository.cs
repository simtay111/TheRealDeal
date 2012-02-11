using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class SportRepository : ISportRepository
    {
        private GraphClient _graphClient;

        public SportRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public SportRepository(): this(GraphClientFactory.Create())
        {
        }

        public Sport FindByName(string name)
        {
            var sport = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport.ToString()).InV<Sport>(n => n.Name == name).FirstOrDefault();

            return sport == null ? null : new Sport(sport.Data.Name);
        }

        public IList<string> GetNamesOfAllSports()
        {
            var sports = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport.ToString()).InV<Sport>().ToList();

            return sports.Select(sport => sport.Data.Name).ToList();
        }

        public void CreateSport(string sportName)
        {
            var sport = new Sport(sportName);

            if (!SportBaseNodeExists())
                CreateSportBaseNode();

            var sportBaseNode = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString()).Single();

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
            return _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString()).Any();
        }
    }
}