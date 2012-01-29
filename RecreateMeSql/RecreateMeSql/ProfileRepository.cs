using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using RecreateMe.Profiles;

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

        public IList<Profile> FindAllByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public IList<Profile> FindByLocation(string locationName)
        {
            throw new System.NotImplementedException();
        }

        public IList<Profile> FindAllBySport(string sport)
        {
            throw new System.NotImplementedException();
        }

        public IList<Profile> FindAllByLocation(string location)
        {
            throw new System.NotImplementedException();
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