using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using TheRealDealTests.DomainTests;

namespace RecreateMeSql
{
    public class ProfileRepository : IProfileRepository
    {
        public Profile GetByProfileId(string profileId)
        {
            return TestData.MockProfile1();
        }

        public bool Save(Profile profile)
        {
            var gc = CreateGraphClient();

            var accountNode =
                gc.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.AccountName == profile.AccountId).Single();
                    //.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>().ToList();

            var profileNode = gc.Create(profile);

            gc.CreateRelationship(accountNode.Reference, new HasProfileRelationship(profileNode));

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

        public IList<Profile> GetByAccount(string accountName)
        {
            var gc = CreateGraphClient();

            var accountNode = gc.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.AccountName == accountName);
            var profileNodes = accountNode.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>().ToList();

            var profileMapper = new ProfileMapper();

            var listOfProfiles = new List<Profile>();

            foreach (var node  in profileNodes)
            {
                listOfProfiles.Add(profileMapper.Map(node));
            }



            //var profiles = TestData.GetListOfMockedProfiles();
            //profiles.RemoveAt(0);
            //return profiles;
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
            throw new System.NotImplementedException();
        }

        private GraphClient CreateGraphClient()
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();
            return graphClient;
        }
    }

    public class GenericNodeType
    {
    }
}