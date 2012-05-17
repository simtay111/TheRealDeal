using System.Configuration;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Configuration;

namespace TheRealDealTests.DomainTests.Configuration
{
    [TestFixture]
    [Category("Integration")]
    public class ConfigurationProviderTests
    {
        private ConfigurationProvider _provider;

        [SetUp]
        public void SetUp()
        {
            _provider = new ConfigurationProvider();
        }

        [Test]
        public void CanGetListOfOneConfigurableProfileOptions()
         {
            const string appOptions = "Sports";
            ConfigurationManager.AppSettings.Set(AppConfigConstants.ProfileOptions, appOptions);

             var options = _provider.GetAllConfigurableProfileOptions();

             Assert.That(options[0], Is.EqualTo(appOptions));
             Assert.That(options.Count, Is.EqualTo(1));

         }

        [Test]
        public void CanParseMultipleOptions()
        {
            const string appOptions = "Sports, Location";
            ConfigurationManager.AppSettings.Set(AppConfigConstants.ProfileOptions, appOptions);

            var options = _provider.GetAllConfigurableProfileOptions();

            Assert.That(options.Count, Is.EqualTo(2));
            Assert.That(options[0], Is.EqualTo("Sports"));
            Assert.That(options[1], Is.EqualTo("Location"));
        }

        [Test]
        public void CanGetFrequencyOfDeleteGameChecks()
        {
            const string frequency = "15";
            ConfigurationManager.AppSettings.Set(AppConfigConstants.FrequencyOfDeleteGameChecks, frequency);

            var value = _provider.GetFrequencyInMinsOfDeleteGameChecks();

            Assert.That(value, Is.EqualTo(15));
        }
    }
}