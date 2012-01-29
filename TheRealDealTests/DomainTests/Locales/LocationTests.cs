using NUnit.Framework;
using RecreateMe.Locales;

namespace TheRealDealTests.DomainTests.Locales
{
    [TestFixture]
    public class LocationTests
    {
        private Location _location;

        [SetUp]
        public void SetUp()
        {
            _location = new Location(1);
        }

        [Test]
        public void MustBeCreatedWithAnId()
        {
            const int id = 5;
            _location = new Location(id);
            Assert.That((object) _location.Id, Is.EqualTo(id));
        }

        [Test]
        public void CanCreateWithAName()
        {
            const string name = "Moo";
            _location = new Location(12, name);
            Assert.That((object) _location.Name, Is.EqualTo(name));
        }

    }
}