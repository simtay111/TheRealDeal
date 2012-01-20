using Moq;
using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace RecreateMe.Tests.Profiles
{
    [TestFixture]
    public class PersonTests
    {
        private Profile _profile;

        [SetUp]
        public void SetUp()
        {
            _profile = new Profile();
        }

        [Test]
        public void CanGetAndSetNameProperty()
        {
            var name = new Mock<Name>().Object;
            _profile.Name = name;
            Assert.That(_profile.Name, Is.SameAs(name));
        }

        [Test]
        public void HoldsAListOfLocationProperties()
        {
            Assert.NotNull(_profile.Locations);
        }

        [Test]
        public void HoldsAListOfSportsWithDifficulties()
        {
            var sport = new Mock<SportWithSkillLevel>().Object;
            _profile.SportsPlayed.Add(sport);
            Assert.That(_profile.SportsPlayed.Count, Is.EqualTo(1));
            Assert.That(_profile.SportsPlayed[0], Is.SameAs(sport));
        }
    }
}
