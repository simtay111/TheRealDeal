using System.Linq;
using NUnit.Framework;
using Neo4jClient;
using RecreateMeSql;
using RecreateMeSql.Connection;
using RecreateMeSql.Repositories;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class BaseRepositoryTests
    {
        private BaseRepository _baseRepo;
        private GraphClient GraphClient = GraphClientFactory.Create();

        [SetUp]
        public void SetUp()
        {
            _baseRepo = new BaseRepository(GraphClient);
        }

        [Test]
        public void CanCreateGameBaseNode()
        {
            _baseRepo.CreateGameBaseNode();

            Assert.True(GraphClient.GameBaseNode().Any());
        }
         
    }
}