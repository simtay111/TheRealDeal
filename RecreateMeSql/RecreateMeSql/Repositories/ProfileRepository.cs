using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.LinkingClasses;
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
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var profile = dbCmd.Select<Profile>(x => x.ProfileName == profileId).SingleOrDefault();

                MapSportsAndLocations(profile, dbCmd);

                return profile;
            }
        }

        public bool Save(Profile profile)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                dbCmd.Insert(profile);
                foreach (var sport in profile.SportsPlayed)
                {
                    dbCmd.Insert(CreatePlayerSportLink(profile, sport));
                }

                foreach (var location in profile.Locations)
                {
                    try
                    {
                        dbCmd.Insert(CreatePlayerLocationLink(profile, location));
                    }
                    catch (SqlException ex)
                    {
                        Trace.WriteLine(ex.Message);
                        return true;
                    }
                }
            }
            return true;
        }

        public bool AddSportToProfile(Profile profile, SportWithSkillLevel sport)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerSportLink = CreatePlayerSportLink(profile, sport);
                dbCmd.Insert(playerSportLink);
                return true;
            }
        }

        public bool AddLocationToProfile(Profile profile, Location location)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerLocationLink = CreatePlayerLocationLink(profile, location);
                dbCmd.Insert(playerLocationLink);
                return true;
            }
        }

        public bool AddFriendToProfile(string profileId, string friendId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Profile> FindAllByName(string name)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var profiles = dbCmd.Select<Profile>(x => x.ProfileName.Contains(name)).ToList();
                foreach (var profile in profiles)
                    MapSportsAndLocations(profile, dbCmd);
                return profiles;
            }
        }

        public IList<Profile> FindAllBySport(string sport)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerIds = dbCmd.Select<PlayerSportLink>(x => x.Sport == sport)
                    .Select(y => y.PlayerId).ToList();


                var profiles = dbCmd.GetByIds<Profile>(playerIds);
                profiles.ForEach(x => MapSportsAndLocations(x, dbCmd));

                return profiles;
            }
        }

        public IList<Profile> FindAllByLocation(string location)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerIds = dbCmd.Select<PlayerLocationLink>(x => x.Location == location)
                    .Select(y => y.PlayerId).ToList();

                var profiles = dbCmd.GetByIds<Profile>(playerIds);
                profiles.ForEach(x => MapSportsAndLocations(x, dbCmd));

                return profiles;
            }
        }

        public IList<Profile> GetByAccount(string accountId)
        {
            using (IDbConnection db = _connectionFactory.OpenDbConnection())
            using (IDbCommand dbCmd = db.CreateCommand())
            {
                var profiles = dbCmd.Select<Profile>(x => x.AccountName == accountId).ToList();
                foreach (var profile in profiles)
                {
                    MapSportsAndLocations(profile, dbCmd);
                }

                return profiles;
            }
        }

        public IList<string> GetFriendsProfileNameList(string profileId)
        {
            throw new System.NotImplementedException();
        }

        public bool ProfileExistsWithName(string profileName)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var profile = dbCmd.Select<Profile>(x => x.ProfileName == profileName).SingleOrDefault();
                return (profile != null);
            }
        }

        public void RemoveSportFromProfile(string profileId, string sportName)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerSportLink = dbCmd.Select<PlayerSportLink>(x => x.PlayerId == profileId
                    && x.Sport == sportName).SingleOrDefault();
                dbCmd.Delete(playerSportLink);
            }
        }

        public void RemoveLocationFromProfile(string profileId, string locationName)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var playerLocationLink = dbCmd.Select<PlayerLocationLink>(x => x.PlayerId == profileId
                    && x.Location == locationName).SingleOrDefault();
                dbCmd.Delete(playerLocationLink);
            }
        }

        public List<Profile> GetProfilesInGame(string gameId)
        {
            using (var db = _connectionFactory.OpenDbConnection())
            using (var dbCmd = db.CreateCommand())
            {
                var profileIds = dbCmd.Select<PlayerInGameLink>(x => x.GameId == gameId)
                    .Select(y => y.PlayerId);

                var profiles = dbCmd.GetByIds<Profile>(profileIds);
                profiles.ForEach(x => MapSportsAndLocations(x, dbCmd));

                return profiles;
            }
        }

        private void MapSportsAndLocations(Profile profile, IDbCommand dbCmd)
        {
            profile.SportsPlayed = dbCmd.Select<PlayerSportLink>("PlayerId = {0}", profile.ProfileName)
                .Select(x => new SportWithSkillLevel { Name = x.Sport, SkillLevel = new SkillLevel(x.Skill) }).ToList();
            profile.Locations = dbCmd.Select<PlayerLocationLink>("PlayerId = {0}", profile.ProfileName)
                .Select(x => new Location() { Name = x.Location }).ToList();
        }

        private PlayerLocationLink CreatePlayerLocationLink(Profile profile, Location location)
        {
            return new PlayerLocationLink
                       {
                           PlayerId = profile.ProfileName,
                           Location = location.Name
                       };
        }

        private PlayerSportLink CreatePlayerSportLink(Profile profile, SportWithSkillLevel sport)
        {
            return new PlayerSportLink
                       {
                           PlayerId = profile.ProfileName,
                           Sport = sport.Name,
                           Skill = sport.SkillLevel.Level
                       };
        }
    }
}