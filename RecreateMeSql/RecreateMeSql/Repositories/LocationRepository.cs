using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.SchemaNodes;

namespace RecreateMeSql.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly GraphClient _graphClient;

        public LocationRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public LocationRepository()
            : this(GraphClientFactory.Create()) { }

        public Location FindByName(string name)
        {
            var locNode = _graphClient.LocationWithName(name).FirstOrDefault();

            return locNode != null ? new Location(locNode.Data.Name) : null;
        }

        public bool CreateLocation(string locationName)
        {
            var location = new Location(locationName);

            if (!LocationBaseNodeExists())
                CreateLocationBaseNode();
            else if (LocationExists(locationName))
                return false;

            var locationBaseNode = _graphClient.LocationBaseNode().Single();

            var locationNode = _graphClient.Create(location);

            _graphClient.CreateRelationship(locationBaseNode.Reference, new LocationRelationship(locationNode));

            return true;
        }

        private void CreateLocationBaseNode()
        {
            var locationBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.LocationBase.ToString() });

            var rootNode = _graphClient.RootNode;

            _graphClient.CreateRelationship(rootNode, new BaseNodeRelationship(locationBaseNode));
        }

        private bool LocationExists(string locationName)
        {
            return _graphClient.LocationWithName(locationName).Any();
        }

        private bool LocationBaseNodeExists()
        {
            return _graphClient.LocationBaseNode().Any();
        }
    }
}