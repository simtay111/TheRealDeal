using NUnit.Framework;
using RecreateMe.Leagues;
using RecreateMe.Organizations;

namespace TheRealDealTests.DomainTests.Organizations
{
    [TestFixture]
    public class OrganizationTests
    {
        [Test]
        public void HoldsAListOfLeagues()
        {
            var organization = new Organization();

            Assert.That(organization.LeagueIds, Is.Empty);
        }
    }
}