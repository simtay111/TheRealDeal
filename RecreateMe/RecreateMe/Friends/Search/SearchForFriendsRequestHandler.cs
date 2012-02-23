using System;
using System.Collections.Generic;
using System.Linq;
using RecreateMe.Profiles;

namespace RecreateMe.Friends.Search
{
    public class SearchForFriendsRequestHandler : IHandler<SearchForFriendsRequest, SearchForFriendsResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public SearchForFriendsRequestHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public SearchForFriendsResponse Handle(SearchForFriendsRequest request)
        {
            var results = PullProfilesFromDatabaseForFirstFieldSpecified(request);

            if (results.Count == 0)
            {
                return new SearchForFriendsResponse(ResponseCodes.NoCriteriaSpecified);
            }

            if (!String.IsNullOrEmpty(request.Sport))   
                results = results.Where(x => (x.SportsPlayed.Where(y => y.Name == request.Sport)).Count() > 0).ToList();

            if (!String.IsNullOrEmpty(request.Location))   
                results = results.Where(x => (x.Locations.Where(y => y.Name == request.Location)).Count() > 0).ToList();

            var selfProfile = results.FirstOrDefault(x => x.ProfileId == request.MyProfile);
            if (selfProfile != null)
                results.Remove(selfProfile);

            return new SearchForFriendsResponse(results);
        }

        private IList<Profile> PullProfilesFromDatabaseForFirstFieldSpecified(SearchForFriendsRequest request)
        {
            IList<Profile> results = new List<Profile>();

            if (!String.IsNullOrEmpty(request.ProfileName))
                results = _profileRepository.FindAllByName(request.ProfileName);
            else
            {
                if (!String.IsNullOrEmpty(request.Sport))
                    results = _profileRepository.FindAllBySport(request.Sport);
                else
                {
                    if (!String.IsNullOrEmpty(request.Location))
                        results = _profileRepository.FindAllByLocation(request.Location);
                }
            }
            return results;
        }
    }

    public class SearchForFriendsRequest
    {
        public string ProfileName { get; set; }
        public string Sport { get; set; }
        public string Location { get; set; }
        public string MyProfile { get; set; }
    }

    public class SearchForFriendsResponse
    {
        public IList<Profile> Results;
        public ResponseCodes Status;

        public SearchForFriendsResponse(ResponseCodes status)
        {
            Status = status;
        }

        public SearchForFriendsResponse(IList<Profile> results)
        {
            Results = results;
            Status = ResponseCodes.Success;
        }
    }
}