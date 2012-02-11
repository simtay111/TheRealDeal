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
            _location = new Location("Bend");
        }

        [Test]
        public void CanCreateWithAName()
        {
            const string name = "Moo";
            _location = new Location(name);
            Assert.That(_location.Name, Is.EqualTo(name));
        }

    }
}