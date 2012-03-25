using System.Linq;
using Neo4jClient;
using RecreateMe.Locales;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.BaseNode;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public LocationRepository(GraphClient graphClient) : base(graphClient)
        {
        }

        public LocationRepository()
            : this(GraphClientFactory.Create()) { }

        public Location FindByName(string name)
        {
            var locNode = GraphClient.LocationWithName(name).FirstOrDefault();

            return locNode != null ? new Location(locNode.Data.Name) : null;
        }

        public bool CreateLocation(string locationName)
        {
            var location = new Location(locationName);

            if (!LocationBaseNodeExists())
                CreateLocationBaseNode();
            else if (LocationExists(locationName))
                return false;

            var locationBaseNode = GraphClient.LocationBaseNode().Single();

            var locationNode = GraphClient.Create(location);

            GraphClient.CreateRelationship(locationBaseNode.Reference, new LocationRelationship(locationNode));

            return true;
        }

        private void CreateLocationBaseNode()
        {
            var locationBaseNode = GraphClient.Create(new SchemaNode { Type = SchemaNodeTypes.LocationBase });

            var rootNode = GraphClient.RootNode;

            GraphClient.CreateRelationship(rootNode, new BaseNodeRelationship(locationBaseNode));
        }

        private bool LocationBaseNodeExists()
        {
            return GraphClient.LocationBaseNode().Any();
        }

        private bool LocationExists(string locationName)
        {
            return GraphClient.LocationWithName(locationName).Any();
        }
    }
}