using System.Collections.Generic;

namespace RecreateMe.Profiles.Search.Handlers
{
    public class SearchForProfileRequestHandle : IHandle<SearchForProfileRequest, SearchForProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;


        public SearchForProfileRequestHandle(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public SearchForProfileResponse Handle(SearchForProfileRequest request)
        {
            IList<Profile> profiles = new List<Profile>();
            if (request.Name != string.Empty)
                profiles = _profileRepository.FindAllByName(request.Name);

            if (profiles.Count == 0)
                return new SearchForProfileResponse(ResponseCodes.NoResultsFound);

            return new SearchForProfileResponse(profiles);
        }
    }

    public class SearchForProfileRequest
    {
        public string Name { get; set; }
    }

    public class SearchForProfileResponse
    {
        public ResponseCodes Status { get; set; }
        public IList<Profile> Profiles;

        public SearchForProfileResponse(IList<Profile> profiles)
        {
            this.Profiles = profiles;
            Status = ResponseCodes.Success;
        }

        public SearchForProfileResponse(ResponseCodes status)
        {
            Status = status;
        }
    }
}