using NUnit.Framework;
using RecreateMeSql;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests
{
    [TestFixture]
    public class ProfileRepositoryTests
    {
        readonly SampleDataBuilder _data = new SampleDataBuilder();
        private ProfileRepository _repo;

        [SetUp]
        public void SetUp()
        {
            _data.DeleteAllData();
            _data.CreateData();
            _repo = new ProfileRepository();
        }

         [Test]
        public void CanGetByAccount()
         {
             Assert.Fail();
         }
    }
}