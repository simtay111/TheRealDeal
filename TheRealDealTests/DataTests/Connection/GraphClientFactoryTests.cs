using NUnit.Framework;
using Neo4jClient;
using RecreateMeSql.Connection;

namespace TheRealDealTests.DataTests.Connection
{
    [TestFixture]
    [Category("Integration")]
    public class GraphClientFactoryTests
    {
        [Test]
        public void ReturnsASingleGraphClientWhenCallingCreate()
        {
            var gc = GraphClientFactory.Create();
            var gc2 = GraphClientFactory.Create();

            Assert.AreSame(gc, gc2);
        }
    }
}