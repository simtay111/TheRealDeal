using System.Collections.Generic;
using RecreateMe.Configuration;

namespace RecreateMe.ProfileSetup.Handlers
{
    public class GetListOfConfigurableProfileOptionsHandler : IHandler<GetListOfConfigurableProfileOptionsRequest, GetListOfConfigurableProfileOptionsResponse>
    {
        private readonly IConfigurationProvider _configurationProvider;

        public GetListOfConfigurableProfileOptionsHandler(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public GetListOfConfigurableProfileOptionsResponse Handle(GetListOfConfigurableProfileOptionsRequest request)
        {
            var response = new GetListOfConfigurableProfileOptionsResponse
                               {
                                   ListOfConfigurableOptions = _configurationProvider.GetAllConfigurableProfileOptions()
                               };


            return response;
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