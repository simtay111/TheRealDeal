using System.Collections.Generic;
using System.Configuration;
using System.Linq;


namespace RecreateMe.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public IList<string> GetAllConfigurableProfileOptions()
        {
            var allOptions = ConfigurationManager.AppSettings["ProfileOptions"];
            
            return allOptions.Split(',').Select(x => x.Trim()).ToList();
        }
    }
}