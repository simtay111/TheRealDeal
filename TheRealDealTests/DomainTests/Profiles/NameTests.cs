using NUnit.Framework;
using RecreateMe.Profiles;

namespace TheRealDealTests.DomainTests.Profiles
{
    [TestFixture]
    public class NameTests
    {
        private Name _name;
        const string First = "Simon";
        const string Last = "Taylor";
            

        [SetUp]
        public void SetUp()
        {
            _name = new Name();
            _name.First = First;
            _name.Last = Last;
        }

        [Test]
        public void HasAFirstAndLastName()
        {
            Assert.AreEqual(_name.First, First);
            Assert.AreEqual(_name.Last, Last);
        }

        [Test]
        public void CanCombineToReturnAFullNameString()
        {
            Assert.That((object) _name.FullName, Is.EqualTo(First + " " + Last));
        }
    }
}