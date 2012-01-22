using Moq;
using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Profiles
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
            Assert.That((object) _profile.Name, Is.SameAs(name));
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
            Assert.That((object) _profile.SportsPlayed.Count, Is.EqualTo(1));
            Assert.That((object) _profile.SportsPlayed[0], Is.SameAs(sport));
        }
    }
}
