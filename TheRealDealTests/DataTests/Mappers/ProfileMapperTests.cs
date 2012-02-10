using NUnit.Framework;
using TheRealDealTests.DomainTests;

namespace TheRealDealTests.DataTests.Mappers
{
    [TestFixture]
    public class ProfileMapperTests
    {
         [Test]
         public void CanMapAProfile()
         {
             var profile = TestData.MockProfile1();

         }
    }
}