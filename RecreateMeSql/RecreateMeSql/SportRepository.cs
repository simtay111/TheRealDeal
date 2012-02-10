using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Sports;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;
using TheRealDealTests.DomainTests;

namespace RecreateMeSql
{
    public class SportRepository : ISportRepository
    {
        public Sport FindByName(string name)
        {
            var gc = CreateGraphClient();

            var sport = gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport.ToString()).InV<Sport>(n => n.Name == name).FirstOrDefault();

            return sport == null ? null : new Sport(sport.Data.Name);
        }

        public IList<string> GetNamesOfAllSports()
        {
            var gc = CreateGraphClient();

            var sports = gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString())
                .OutE(RelationsTypes.Sport.ToString()).InV<Sport>().ToList();

            return sports.Select(sport => sport.Data.Name).ToList();
        }

        public void CreateSport(string sportName)
        {


            var sport = new Sport(sportName);

            var gc = CreateGraphClient();

            if (!SportBaseNodeExists(gc))
                CreateSportBaseNode(gc);

            var sportBaseNode = gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString()).Single();

            var sportNode = gc.Create(sport);

            gc.CreateRelationship(sportBaseNode.Reference, new SportRelationship(sportNode));
        }

        private void CreateSportBaseNode(GraphClient gc)
        {
            var sportBaseNode = gc.Create(new SchemaNode {Type = SchemaNodeTypes.SportBase.ToString()});

            var rootNode = gc.RootNode;

            gc.CreateRelationship(rootNode, new BaseNodeRelationship(sportBaseNode));
        }

        private bool SportBaseNodeExists(GraphClient gc)
        {
            return gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString()).Any();
        }

        private GraphClient CreateGraphClient()
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();
            return graphClient;
        }
    }
}