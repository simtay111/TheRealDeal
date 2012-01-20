using NUnit.Framework;
using RecreateMe.Locales;

namespace RecreateMe.Tests.Locales
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
        public void HasAListOfChildLocationsThatItEncompasses()
        {
            Assert.NotNull(_location.SubLocations);
        }

        [Test]
        public void MustBeCreatedWithAnId()
        {
            const int id = 5;
            _location = new Location(id);
            Assert.That(_location.Id, Is.EqualTo(id));
        }

        [Test]
        public void CanCreateWithAName()
        {
            const string name = "Moo";
            _location = new Location(12, name);
            Assert.That(_location.Name, Is.EqualTo(name));
        }

        [Test]
        public void CanAddChildLocations()
        {
            var sublocation1 = new Location(32);
            _location.SubLocations.Add(sublocation1);
            Assert.That(_location.SubLocations[0], Is.SameAs(sublocation1));
        }
    }
}