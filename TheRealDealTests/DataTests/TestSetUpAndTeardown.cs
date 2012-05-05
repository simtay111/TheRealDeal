using NUnit.Framework;

namespace TheRealDealTests.DataTests
{
    [SetUpFixture]
    public class TestSetUpAndTeardown
    {
        [SetUp]
        public void GlobalSetUp()
        {
            SqlServerDataHelper.RebuildSchema();
        }

        [TearDown]
        public void TearDown()
        {
            
        }
         
    }
}