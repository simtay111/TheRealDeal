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
            ConnectionFactory.SetConnectionString(@"Server=(local);Database=PortlandPickUp;Trusted_Connection=True;");
            SqlServerDataHelper.RebuildSchema();
        }

        [TearDown]
        public void TearDown()
        {
            
        }
         
    }
}