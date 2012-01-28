using System.Collections.Generic;

namespace RecreateMe.Configuration
{
    public interface IConfigurationProvider
    {
        IList<string> GetAllConfigurableProfileOptions();
    }
}