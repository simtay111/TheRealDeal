using Moq;
using NUnit.Framework;
using RecreateMe.Configuration;
using RecreateMe.GameMaintenance;

namespace TheRealDealTests.DomainTests.GameMaintenance
{
    [TestFixture]
    public class TimedGameDeleterTests
    {
        [Test]
        public void UsesATimerToDeleteOldGames()
        {
            var configProvider = new Mock<IConfigurationProvider>();
            configProvider.Setup(x => x.GetFrequencyInMinsOfDeleteGameChecks()).Returns(15);

            var oldGameRemover = new Mock<IOldGameRemover>();

            var deleter = new TimedGameDeleter(configProvider.Object, oldGameRemover.Object );

            deleter.BeginDeleting();
        }
    }
}