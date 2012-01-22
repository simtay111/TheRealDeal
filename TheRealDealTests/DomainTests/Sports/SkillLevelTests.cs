using NUnit.Framework;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Sports
{
    [TestFixture]
    public class SkillLevelTests
    {
        private SkillLevel _skillLevel;

        [SetUp]
        public void SetUp()
        {
            _skillLevel = new SkillLevel();
        }

        [Test]
        public void HoldsASkillLevel()
        {
            const int level = 5;
            _skillLevel.Level = level;
            Assert.That((object) _skillLevel.Level, Is.EqualTo(level));
        }

        [Test]
        public void CanBeCreatedWithASkillLevel()
        {
            const int level = 5;
            _skillLevel = new SkillLevel(5);
            Assert.AreEqual(level, _skillLevel.Level);
        }

        [Test]
        public void UsesADefaultSkillLevelIfItIsCreatedWithAZeroSkillLevel()
        {
            const int level = 0;
            _skillLevel = new SkillLevel(level);
        }
    }
}