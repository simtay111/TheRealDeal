using System.Collections.Generic;
using RecreateMe.Configuration;

namespace RecreateMe.ProfileSetup.Handlers
{
    public class GetListOfConfigurableProfileOptionsHandle : IHandle<GetListOfConfigurableProfileOptionsRequest, GetListOfConfigurableProfileOptionsResponse>
    {
        private readonly IConfigurationProvider _configurationProvider;

        public GetListOfConfigurableProfileOptionsHandle(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public GetListOfConfigurableProfileOptionsResponse Handle(GetListOfConfigurableProfileOptionsRequest request)
        {
            return new GetListOfConfigurableProfileOptionsResponse
            {
                ListOfConfigurableOptions = _configurationProvider.GetAllConfigurableProfileOptions()
            };
        }
    }

    public class GetListOfConfigurableProfileOptionsRequest
    {
        
    }

    public class GetListOfConfigurableProfileOptionsResponse
    {
        public IList<string> ListOfConfigurableOptions { get; set; }
    }
}