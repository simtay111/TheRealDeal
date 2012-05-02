using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using RecreateMeSql.Relationships.AccountRelationships;
using RecreateMeSql.Relationships.ProfileRelationships;

namespace RecreateMeSql.Repositories
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        private readonly ProfileMapper _profileMapper;

        public ProfileRepository(GraphClient graphClient, ProfileMapper profileMapper) : base(graphClient)
        {
            _profileMapper = profileMapper;
        }

        public ProfileRepository()
            : this(GraphClientFactory.Create(), new ProfileMapper())
        {
        }

        public Profile GetByProfileId(string profileId)
        {
            var profile = GraphClient.ProfileWithId(profileId).Single();

            return _profileMapper.Map(profile);
        }

        public bool Save(Profile profile)
        {
            var accountNode = GraphClient.AccountWithId(profile.AccountId).Single();

            var profileNode = GraphClient.Create(profile);

            GraphClient.CreateRelationship(accountNode.Reference, new HasProfileRelationship(profileNode));

            CreateLocationRelationships(profile, profileNode);
            CreateSportRelationshipsForNewProfile(profile, profileNode);

            return true;
        }

        public bool AddSportToProfile(Profile profile, SportWithSkillLevel sport)
        {
            var profileNode = GraphClient.ProfileWithId(profile.ProfileId).Single();

            CreateSingleSportRelationship(profileNode.Reference, sport);

            return true;
        }

        public bool AddLocationToProfile(Profile profile, Location location)
        {
            var profileNode = GraphClient.ProfileWithId(profile.ProfileId).Single();

            CreateSingleLocationRelationship(profileNode.Reference, location);

            return true;
        }

        public bool AddFriendToProfile(string profileId, string friendId)
        {

            var profileNode = GraphClient.ProfileWithId(profileId).Single();
            var friendNode = GraphClient.ProfileWithId(friendId).Single();

            CreateFriendRelationship(profileNode, friendNode);

            return true;
        }

        public IList<Profile> FindAllByName(string name)
        {
            var profileNodes = GraphClient.Accounts().Profiles().ToList();

            profileNodes = profileNodes.Where(x => x.Data.ProfileId.Contains(name)).ToList();

            var profiles = new List<Profile>();

            profileNodes.ForEach(x => profiles.Add(_profileMapper.Map(x)));

            return profiles;
        }

        public IList<Profile> FindAllBySport(string sport)
        {
            var sportNode = GraphClient.SportWithName(sport);

            var profileNodes = sportNode.InE(RelationsTypes.ProfileSport).OutV<Profile>().ToList();

            var profiles = new List<Profile>();
            profileNodes.ForEach(node => profiles.Add(_profileMapper.Map(node)));

            return profiles;
        }

        public IList<Profile> FindAllByLocation(string location)
        {
            var locationNode = GraphClient.LocationWithName(location);

            var profileNodes = locationNode.InE(RelationsTypes.ProfileLocation).OutV<Profile>().ToList();

            var profiles = new List<Profile>();
            profileNodes.ForEach(node => profiles.Add(_profileMapper.Map(node)));

            return profiles;
        }

        public IList<Profile> GetByAccount(string accountId)
        {
            var profileNodes = GraphClient.AccountWithId(accountId).Profiles().ToList();

            return profileNodes.Select(node => _profileMapper.Map(node)).ToList();
        }

        public IList<string> GetFriendsProfileNameList(string profileId)
        {
            var profileNode = GraphClient.ProfileWithId(profileId);

            var friendNodes = profileNode.OutE(RelationsTypes.Friend).InV<Profile>().ToList();

            return friendNodes.Select(x => x.Data.ProfileId).ToList();
        }

        public bool ProfileExistsWithName(string profileName)
        {
            var nodes = GraphClient.ProfileWithId(profileName);

            return nodes.Any();
        }

        public void RemoveSportFromProfile(string profileId, string sportName)
        {
            var sportNode = GraphClient.SportWithName(sportName).Single();
            var relationship = GraphClient.ProfileWithId(profileId).OutE().Where(x => x.EndNodeReference == sportNode.Reference).Single();
            GraphClient.DeleteRelationship(relationship.Reference);
        }

        public void RemoveLocationFromProfile(string profileId, string locationName)
        {
            var locNode = GraphClient.LocationWithName(locationName).Single();
            var relationship = GraphClient.ProfileWithId(profileId).OutE().Where(x => x.EndNodeReference == locNode.Reference).Single();
            GraphClient.DeleteRelationship(relationship.Reference);
        }

        private void CreateLocationRelationships(Profile profile, NodeReference<Profile> profileNode)
        {
            foreach (var location in profile.Locations)
            {
                CreateSingleLocationRelationship(profileNode, location);
            }
        }

        private void CreateFriendRelationship(Node<Profile> profileNode, Node<Profile> friendNode)
        {
            GraphClient.CreateRelationship(profileNode.Reference, new FriendRelationship(friendNode.Reference));
        }

        private void CreateSingleLocationRelationship(NodeReference<Profile> profileNode, Location location)
        {
            var locNode = GraphClient.LocationWithName(location.Name).FirstOrDefault();
            if (locNode == null) return;
            GraphClient.CreateRelationship(profileNode, new ProfileToLocationRelationship(locNode.Reference));
        }

        private void CreateSportRelationshipsForNewProfile(Profile profile, NodeReference<Profile> profileNode)
        {

            foreach (var sport in profile.SportsPlayed)
            {
                CreateSingleSportRelationship(profileNode, sport);
            }
        }

        private void CreateSingleSportRelationship(NodeReference<Profile> profileNode, SportWithSkillLevel sport)
        {
            var sportNode = GraphClient.SportWithName(sport.Name).FirstOrDefault();
            if (sportNode == null) return;
            var profileToSportRelationship = new ProfileToSportRelationship(sportNode.Reference, sport.SkillLevel);
            GraphClient.CreateRelationship(profileNode,
                                            profileToSportRelationship);
        }

        public List<Profile> GetProfilesInGame(string gameId)
        {
            var thingy = GraphClient.GameWithId(gameId).InE(RelationsTypes.PlaysInGame).OutV<Profile>();

            return thingy.Select(x => _profileMapper.Map(x)).ToList();
        }
    }
}