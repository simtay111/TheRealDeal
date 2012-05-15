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
            ConnectionFactory.SetConnectionString(@"Server=e68a7e51-aaa0-4f84-9b24-a052015234b6.sqlserver.sequelizer.com;Database=dbe68a7e51aaa04f849b24a052015234b6;User ID=lthacnqfszvhnhoa;Password=JxCbALoiiQoHc8pi5uurZH3CPUNvhSNNttonPvwXEi2yqdjTPQvk7rhbC8uN7Zax;");
            SqlServerDataHelper.RebuildSchema();
        }

        [TearDown]
        public void TearDown()
        {
            
        }
         
    }
}