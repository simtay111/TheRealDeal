using NUnit.Framework;
using RecreateMe;
using RecreateMe.Teams;

namespace TheRealDealTests.DomainTests.Teams
{
    [TestFixture]
    public class TeamTests
    {
        [Test]
        public void HoldAMaxSizeAndDefaultsToOne()
        {
            var team = new Team();
            Assert.AreEqual((object) Constants.DefaultTeamSize, team.MaxSize);
            team.MaxSize = 5;
            Assert.AreEqual((object) 5, team.MaxSize);
        }

        [Test]
        public void HoldsAListOfPlayers()
        {
            var team = new Team();
            Assert.NotNull(team.Players);
        }
         
        [Test]
        public void HasANameAndHasADefaultName()
        {
            const string name = "Big Team";
            var team = new Team {Name = name};
            Assert.AreEqual(name, team.Name);
            team = new Team();
            Assert.AreEqual((object) Constants.DefaultTeamName, team.Name);
        }

        [Test]
        public void EveryGameHasAGameIdThatIsAGuidCreatedAtCreationgOfGame()
        {
            var team = new Team();
            Assert.NotNull(team.Id);
        }
    }
}