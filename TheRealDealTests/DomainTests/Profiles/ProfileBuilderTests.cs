using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Profiles
{
    [TestFixture]
    public class ProfileBuilderTests
    {
        private ProfileBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new ProfileBuilder();
        }

        [Test]
        public void BuildReturnsAProfile()
        {
            var builder = CreateAMockBuilder();
            builder.Setup(x => x.Sport).Returns(new Mock<Sport>().Object);
            _builder = builder.Object;
            var profile = _builder.Build();
            Assert.NotNull(profile);
        }

        [Test]
        public void WithNameSetsNameOnProfile()
        {
            var profileId = "Myname";
            var builder = _builder.WithProfileId(profileId);
            Assert.That((object) builder.ProfileId, Is.EqualTo(profileId));
        }

        [Test]
        public void WithLocationSetsLocationOnProfile()
        {
            var location = new Mock<Location>(1).Object;
            var builder = _builder.WithLocation(location);
            Assert.NotNull(builder.Location);
        }

        [Test]
        public void CreatingLocationWithNullAssignsDefaultLocation()
        {
            var profileId = "Myname";
            var profile = _builder
                .WithLocation(null)
                .WithProfileId(profileId)
                .Build();
            Assert.AreEqual(profile.Locations.Count, 1);
            Assert.IsInstanceOf(typeof(Location), profile.Locations[0]);
        }

        [Test]
        public void CanCreateWithSport()
        {
            CreateAMockBuilder();
            var sport = new Mock<SportWithSkillLevel>().Object;
            _builder.WithSport(sport);
            Assert.AreSame(_builder.Sport, sport);
        }

        [Test]
        public void CanCreateWithSkillLevel()
        {
            var builder =  CreateAMockBuilder();
            builder.Setup(x => x.Sport).Returns(new Mock<Sport>().Object);
            _builder = builder.Object;
            var skillLevel = new Mock<SkillLevel>().Object;
            _builder.WithSkillLevel(skillLevel);
            Assert.AreSame(_builder.LevelOfSkill, skillLevel);
        }

        [Test]
        public void LeavesSportEmptyIfNullPassedIn()
        {
            _builder = CreateAMockBuilder().Object;
            _builder.WithSport(null);
            var person = _builder.Build();
            Assert.That((object) person.SportsPlayed.Count, Is.EqualTo(0));
        }

        [Test]
        public void SkillLevelIsSetToDefaultIfNotSpecified()
        {
            _builder = CreateAMockBuilder().Object;
            _builder.WithSport(new Mock<Sport>().Object);
            _builder.WithSkillLevel(null);
            var profile = _builder.Build();
            Assert.AreEqual(profile.SportsPlayed[0].SkillLevel.Level, Constants.DefaultSkillLevel);
        }

        private Mock<ProfileBuilder> CreateAMockBuilder()
        {
            var builder = new Mock<ProfileBuilder>() {CallBase = true};
            builder.Setup(x => x.ProfileId).Returns(string.Empty);
            builder.Setup(x => x.Location).Returns(new Mock<Location>(1).Object);
            return builder;
        }
    }
}