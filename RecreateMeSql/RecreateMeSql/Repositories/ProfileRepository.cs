using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;

namespace RecreateMeSql.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly GraphClient _graphClient;
        private readonly ProfileMapper _profileMapper;

        public ProfileRepository(GraphClient graphClient, ProfileMapper profileMapper)
        {
            _graphClient = graphClient;
            _profileMapper = profileMapper;
        }

        public ProfileRepository() : this(GraphClientFactory.Create(), new ProfileMapper())
        {
        }

        public Profile GetByProfileId(string profileId)
        {
            return TestData.MockProfile1();
        }

        public bool Save(Profile profile)
        {
            var accountNode = _graphClient.AccountWithId(profile.AccountId).Single();

            var profileNode = _graphClient.Create(profile);

            _graphClient.CreateRelationship(accountNode.Reference, new HasProfileRelationship(profileNode));

            CreateLocationRelationships(profile, profileNode);

            return true;
        }

        public bool AddSportToProfile(Profile profile, Sport sport)
        {
            return true;
        }

        public bool AddLocationToProfile(Profile profile, Location location)
        {
            return true;
        }

        public bool AddFriendToProfile(Profile profile, Profile friend)
        {
            return true;
        }

        public IList<Profile> FindAllByName(string name)
        {
            return TestData.GetListOfMockedProfiles();
        }

        public IList<Profile> FindAllBySport(string sport)
        {
            return TestData.GetListOfMockedProfiles();
        }

        public IList<Profile> FindAllByLocation(string location)
        {
            return TestData.GetListOfMockedProfiles();
        }

        public IList<Profile> GetByAccount(string accountId)
        {
            var profileNodes = _graphClient.AccountWithId(accountId).Profiles().ToList();

            return profileNodes.Select(node => _profileMapper.Map(node)).ToList();
        }

        public Dictionary<string, string> GetFriendIdAndNameListForProfile(string profileId)
        {
            var friend1 = TestData.MockProfile2();
            var friend2 = TestData.MockProfile3();
            return new Dictionary<string, string> {{friend1.ProfileId, friend1.ProfileId}, {friend2.ProfileId, friend2.ProfileId}};
        }

        public bool ProfileExistsWithName(string profileName)
        {
            var nodes = _graphClient.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>()
                .OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>(n => n.ProfileId == profileName);

            return nodes.Any();
        }

        private void CreateLocationRelationships(Profile profile, NodeReference<Profile> profileNode)
        {
            foreach (var location in profile.Locations)
            {
                var locNode = _graphClient.LocationWithName(location.Name).FirstOrDefault();
                if (locNode == null) continue;
                _graphClient.CreateRelationship(profileNode, new ProfileToLocationRelationship(locNode.Reference));
            }
        }
    }
}