
namespace RecreateMe.Organizations.Handlers
{
    public class CreateOrganizationRequestHandle : IHandle<CreateOrganizationRequest, CreateOrganizationResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public CreateOrganizationRequestHandle(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public CreateOrganizationResponse Handle(CreateOrganizationRequest request)
        {
            var organization = new Organization
                                   {
                                       Name = request.Name,
                                       CreatorId = request.ProfileId
                                   };

            _organizationRepository.Save(organization);

            return new CreateOrganizationResponse();
        }
    }

    public class CreateOrganizationRequest
    {
        public string Name { get; set; }

        public string ProfileId { get; set; }
    }

    public class CreateOrganizationResponse
    {
    }
}