using NUnit.Framework;
using RecreateMe.Leagues;

namespace TheRealDealTests.DomainTests.Leagues
{
    [TestFixture]
    public class LeagueTests
    {
        [Test]
        public void HoldsAListOfSubLeagues()
        {
            var league = new League();

            Assert.That(league.ChildLeagues, Is.Empty);
        }
         
    }
}