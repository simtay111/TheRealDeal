using System.Linq;
using Neo4jClient.Gremlin;
using NUnit.Framework;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.Repositories;
using RecreateMeSql.SchemaNodes;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class LocationRepositoryTests
    {
        private LocationRepository _repo;
        private readonly SampleDataBuilder _data = new SampleDataBuilder();

        [SetUp]
        public void SetUp()
        {
            _data.DeleteAllData();
            _repo = new LocationRepository();
        }

        [Test]
        public void CanFindByName()
        {
            _data.CreateLocationBend();
            const string locationname = "Bend";

            var location = _repo.FindByName(locationname);

            Assert.That(location.Name, Is.EqualTo(locationname));
        }

        [Test]
        public void CanCreateLocations()
        {
            const string locationname = "Bend";

            _repo.CreateLocation(locationname);

            var location = _repo.FindByName(locationname);

            Assert.That(location.Name, Is.EqualTo(locationname));
        }

        [Test]
        public void CreatesLocationSchemaNodeIfItDoesntExistWhenCreatingLocations()
        {
            _repo.CreateLocation("Bend");

            var gc = GraphClientFactory.Create();
            var nodes = gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.LocationBase.ToString());
            Assert.True(nodes.Any());
        }

        [Test]
        public void WillNotCreateLocationIfItAlreadyExists()
        {
            const string locationName = "Bend";
            _repo.CreateLocation(locationName);

            var wasSuccessful =  _repo.CreateLocation(locationName);

            Assert.False(wasSuccessful);
        }
         
    }
}