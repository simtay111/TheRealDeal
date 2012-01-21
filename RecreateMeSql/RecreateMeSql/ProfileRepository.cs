using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using RecreateMe.Profiles;

namespace RecreateMeSql
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly IJsonDataAccess _jsonDataAccess;

        public ProfileRepository(IJsonDataAccess jsonDataAccess)
        {
            _jsonDataAccess = jsonDataAccess;
        }

        public Profile GetByUniqueId(string uniqueId)
        {
            return _jsonDataAccess.GetByFileName<Profile>(uniqueId);
        }

        public bool SaveOrUpdate(Profile profile)
        {
            _jsonDataAccess.WriteToJson(profile, profile.UniqueId);
            
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
    }
}