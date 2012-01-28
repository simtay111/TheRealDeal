using NUnit.Framework;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Sports
{
    [TestFixture]
    public class SkillLevelProviderTests
    {
        [Test]
        public void CanGetListOfAvailableSkillLevels()
        {
            var provider = new SkillLevelProvider();

            var listOfSkillLevels = provider.GetListOfAvailableSkillLevels();

            Assert.That(listOfSkillLevels.Count, Is.EqualTo(10));
        }
    }
}