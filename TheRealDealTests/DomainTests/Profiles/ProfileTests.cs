using Moq;
using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Profiles
{
    [TestFixture]
    public class ProfileTests
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

        [Test]
        public void CanGetFullAccountName()
        {
            var name = new Name("Simon", "Taylor");
            _profile.Name = name;
            _profile.AccountId = "Moo@Moo.com";

            Assert.That(_profile.FullAccountName, Is.EqualTo("Simon Taylor (Moo@Moo.com)"));
        }
    }
}
