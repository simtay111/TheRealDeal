using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class SportRepository : BaseRepository, ISportRepository
    {
        public SportRepository(GraphClient graphClient) : base(graphClient)
        {
        }

        public SportRepository()
            : this(GraphClientFactory.Create()) { }

        public Sport FindByName(string name)
        {
            var sport = GraphClient.SportWithName(name).FirstOrDefault();

            return sport == null ? null : new Sport(sport.Data.Name);
        }

        public IList<string> GetNamesOfAllSports()
        {
            return GraphClient.AllSportNodes().Select(sport => sport.Data.Name).ToList();
        }

        public void CreateSport(string sportName)
        {
            var sport = new Sport(sportName);

            CreateSportBaseNode();

            var sportBaseNode = GraphClient.SportBaseNode().Single();
            var sportNode = GraphClient.Create(sport);

            GraphClient.CreateRelationship(sportBaseNode.Reference, new SportRelationship(sportNode));
        }
    }
}