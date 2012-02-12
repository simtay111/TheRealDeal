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

            return locNode != null ? new Location(locNode.Data.Name) : null;
        }

        public bool CreateLocation(string locationName)
        {
            var location = new Location(locationName);

            if (!LocationBaseNodeExists())
                CreateLocationBaseNode();
            else if (LocationExists(locationName))
                return false;

            var locationBaseNode = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString()).Single();

            var locationNode = _graphClient.Create(location);

            _graphClient.CreateRelationship(locationBaseNode.Reference, new LocationRelationship(locationNode));

            return true;
        }

        private bool LocationExists(string locationName)
        {
            var locationBase = _graphClient.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString());

            var locNode = locationBase.OutE(RelationsTypes.Location.ToString())
                .InV<Location>(n => n.Name == locationName);

            return (locNode.Any());
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