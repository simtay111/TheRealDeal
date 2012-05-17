using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace RecreateMe.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public IList<string> GetAllConfigurableProfileOptions()
        {
            var allOptions = ConfigurationManager.AppSettings[AppConfigConstants.ProfileOptions];
            
            return allOptions.Split(',').Select(x => x.Trim()).ToList();
        }

        public int GetFrequencyInMinsOfDeleteGameChecks()
        {
            var frequency = ConfigurationManager.AppSettings[AppConfigConstants.FrequencyOfDeleteGameChecks];

            return int.Parse(frequency);
        }
    }
}