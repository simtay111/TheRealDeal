using System.Collections.Generic;
using System.Data;
using System.Linq;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using ServiceStack.OrmLite;

namespace RecreateMeSql.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly OrmLiteConnectionFactory _connectionFactory;

        public ProfileRepository(OrmLiteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public ProfileRepository()
            : this(ConnectionFactory.Create())
        {
        }

        public Profile GetByProfileId(string profileId)
        {
            throw new System.NotImplementedException();
        }

        public bool Save(Profile profile)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                dbCmd.Insert(profile);
            }
            return true;
        }

        public bool AddSportToProfile(Profile profile, SportWithSkillLevel sport)
        {
            throw new System.NotImplementedException();
        }

        public bool AddLocationToProfile(Profile profile, Location location)
        {
            throw new System.NotImplementedException();
        }

        public bool AddFriendToProfile(string profileId, string friendId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Profile> FindAllByName(string name)
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

        public IList<Profile> GetByAccount(string accountId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                return dbCmd.Each<Profile>("AccountName = {0}", accountId).ToList();
            }
        }

        public IList<string> GetFriendsProfileNameList(string profileId)
        {
            throw new System.NotImplementedException();
        }

        public bool ProfileExistsWithName(string profileName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSportFromProfile(string profileId, string sportName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveLocationFromProfile(string profileId, string locationName)
        {
            throw new System.NotImplementedException();
        }

        public List<Profile> GetProfilesInGame(string gameId)
        {
            throw new System.NotImplementedException();
        }
    }
}