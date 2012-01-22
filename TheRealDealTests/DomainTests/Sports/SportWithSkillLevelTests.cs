using Moq;
using NUnit.Framework;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Sports
{
    [TestFixture]
    public class SportWithSkillLevelTests
    {
        private SportWithSkillLevel _sport;

        [SetUp]
        public void SetUp()
         {
             _sport = new SportWithSkillLevel();
         }

        [Test]
        public void IsAnInstanceOfSport()
        {
            Assert.That(_sport, Is.InstanceOf<Sport>());
        }

        [Test]
        public void HasASkillLevel()
        {
            Assert.That(_sport, Is.InstanceOf<SportWithSkillLevel>());
            var skillLevel = new Mock<SkillLevel>().Object;
            _sport.SkillLevel = skillLevel;
            Assert.That(_sport.SkillLevel, Is.SameAs(skillLevel));
        }
    }
}