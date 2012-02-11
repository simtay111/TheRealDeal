using System;
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
        private GraphClient _graphClient;

        public ProfileRepository(GraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public ProfileRepository() : this(GraphClientFactory.Create())
        {
        }

        public Profile GetByProfileId(string profileId)
        {
            return TestData.MockProfile1();
        }

        public bool Save(Profile profile)
        {
            var accountNode =
                _graphClient.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.AccountName == profile.AccountId).Single();
                    //.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>().ToList();

            var profileNode = _graphClient.Create(profile);

            _graphClient.CreateRelationship(accountNode.Reference, new HasProfileRelationship(profileNode));

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
            var accountNode = _graphClient.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.AccountName == accountId);
            var profileNodes = accountNode.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>().ToList();

            var profileMapper = new ProfileMapper();

            var listOfProfiles = new List<Profile>();

            foreach (var node  in profileNodes)
            {
                listOfProfiles.Add(profileMapper.Map(node));
            }

            return listOfProfiles;
        }

        public Dictionary<string, string> GetFriendIdAndNameListForProfile(string profileId)
        {
            var friend1 = TestData.MockProfile2();
            var friend2 = TestData.MockProfile3();
            return new Dictionary<string, string>() {{friend1.ProfileId, friend1.ProfileId}, {friend2.ProfileId, friend2.ProfileId}};
        }

        public bool ProfileExistsWithName(string profileName)
        {
            var nodes = _graphClient.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>()
                .OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>(n => n.ProfileId == profileName);

            return nodes.Any();
        }
    }
}