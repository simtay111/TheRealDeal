namespace RecreateMe.Organizations.Handlers
{
    public class AddLeagueToOrganizationRequestHandler : IHandler<AddLeagueToOrganizationRequest, AddLeagueToOrganizationResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public AddLeagueToOrganizationRequestHandler(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public AddLeagueToOrganizationResponse Handle(AddLeagueToOrganizationRequest request)
        {
            var org = _organizationRepository.GetById(request.OrganizationId);

            if (org.LeagueIds.Contains(request.LeagueId))
                return new AddLeagueToOrganizationResponse {Status = ResponseCodes.AlreadyInLeague};

            org.LeagueIds.Add(request.LeagueId);

            _organizationRepository.Save(org);

            return new AddLeagueToOrganizationResponse {Status = ResponseCodes.Success};
        }
    }

    public class AddLeagueToOrganizationRequest
    {
        public string OrganizationId { get; set; }

        public string LeagueId { get; set; }
    }

    public class AddLeagueToOrganizationResponse
    {
        public ResponseCodes Status { get; set; }
    }
}