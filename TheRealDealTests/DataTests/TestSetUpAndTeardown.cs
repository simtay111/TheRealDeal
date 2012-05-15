using System;
using System.Configuration;
using System.IO;
using NUnit.Framework;
using RecreateMeSql;

namespace TheRealDealTests.DataTests
{
    [SetUpFixture]
    public class TestSetUpAndTeardown
    {
        [SetUp]
        public void GlobalSetUp()
        {
            //Server=(local);Database=PortlandPickUp;Trusted_Connection=True;
            ConnectionFactory.SetConnectionString(@"Server=e2babdc8-29c6-4349-a215-a04b000468e0.sqlserver.sequelizer.com;Database=dbe2babdc829c64349a215a04b000468e0;User ID=lyyfhvctjjrscbps;Password=XhHC4xWZbpWheqSSQCJbJfTB6YPctfUoHgZxncwcihmcc8QeK5bKAJhRvfFeRUF4;");
            SqlServerDataHelper.RebuildSchema();

            
        }

        [TearDown]
        public void TearDown()
        {
            
        }
         
    }
}