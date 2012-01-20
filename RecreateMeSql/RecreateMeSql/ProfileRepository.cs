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
            throw new System.NotImplementedException();
        }

        public bool SaveOrUpdate(Profile profile)
        {
            using (var writer = new StreamWriter(String.Format(@"C:\Test\{0}.txt", profile.UniqueId)))
            {
                writer.AutoFlush = true;

                Json.Write(profile, writer);
            }
            
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