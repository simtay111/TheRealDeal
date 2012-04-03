
namespace RecreateMe.Organizations.Handlers
{
    public class CreateOrganizationRequestHandler : IHandler<CreateOrganizationRequest, CreateOrganizationResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public CreateOrganizationRequestHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public CreateOrganizationResponse Handle(CreateOrganizationRequest request)
        {
            var organization = new Organization
                                   {
                                       Name = request.Name
                                   };

            _organizationRepository.Save(organization);

            return new CreateOrganizationResponse();
        }
    }

    public class CreateOrganizationRequest
    {
        public string Name { get; set; }
    }

    public class CreateOrganizationResponse
    {
    }
}