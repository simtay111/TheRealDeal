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
            : this(GraphClientFactory.Create())
        {
        }

        public Location FindByName(string name)
        {
            var locBaseNode = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString());

            var locNode = locBaseNode.OutE(RelationsTypes.Location.ToString())
                .InV<Location>(n => n.Name == name).FirstOrDefault();

            return locNode != null ? new Location(locNode.Data.Id, locNode.Data.Name) : null;
        }

        public void CreateLocation(string locationName)
        {
            var location = new Location(97702, locationName);

            if (!LocationBaseNodeExists())
                CreateLocationBaseNode();

            var locationBaseNode = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString()).Single();

            var locationNode = _graphClient.Create(location);

            _graphClient.CreateRelationship(locationBaseNode.Reference, new LocationRelationship(locationNode));
        }

        private void CreateLocationBaseNode()
        {
            var locationBaseNode = _graphClient.Create(new SchemaNode { Type = SchemaNodeTypes.LocationBase.ToString() });

            var rootNode = _graphClient.RootNode;

            _graphClient.CreateRelationship(rootNode, new BaseNodeRelationship(locationBaseNode));
        }

        private bool LocationBaseNodeExists()
        {
            return _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString()).Any();
        }
    }
}