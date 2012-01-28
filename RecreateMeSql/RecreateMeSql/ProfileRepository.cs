using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using RecreateMe.Profiles;

namespace RecreateMeSql
{
    public class ProfileRepository : IProfileRepository
    {
        public Profile GetByUniqueId(string uniqueId)
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
    }
}