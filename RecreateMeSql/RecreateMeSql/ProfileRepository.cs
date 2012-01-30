using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace RecreateMeSql
{
    public class ProfileRepository : IProfileRepository
    {
        public Profile GetByProfileId(string profileId)
        {
            return TestData.MockProfile1();
        }

        public bool SaveOrUpdate(Profile profile)
        {
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
            var profiles = TestData.GetListOfMockedProfiles();
            profiles.RemoveAt(0);
            return profiles;
        }

        public Dictionary<string, Name> GetFriendIdAndNameListForProfile(string profileId)
        {
            var friend1 = TestData.MockProfile2();
            var friend2 = TestData.MockProfile3();
            return new Dictionary<string, Name>() {{friend1.UniqueId, friend1.Name}, {friend2.UniqueId, friend2.Name}};
        }
    }
}