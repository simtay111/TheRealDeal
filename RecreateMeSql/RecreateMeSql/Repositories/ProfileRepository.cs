using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;

namespace RecreateMeSql.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly GraphClient _graphClient;
        private readonly ProfileMapper _profileMapper;

        public ProfileRepository(GraphClient graphClient, ProfileMapper profileMapper)
        {
            _graphClient = graphClient;
            _profileMapper = profileMapper;
        }

        public ProfileRepository()
            : this(GraphClientFactory.Create(), new ProfileMapper())
        {
        }

        public Profile GetByProfileId(string profileId)
        {
            var profile = _graphClient.ProfileWithId(profileId).Single();

            return _profileMapper.Map(profile);
        }

        public bool Save(Profile profile)
        {
            var accountNode = _graphClient.AccountWithId(profile.AccountId).Single();

            var profileNode = _graphClient.Create(profile);

            _graphClient.CreateRelationship(accountNode.Reference, new HasProfileRelationship(profileNode));

            CreateLocationRelationships(profile, profileNode);
            CreateSportRelationshipsForNewProfile(profile, profileNode);

            return true;
        }

        public bool AddSportToProfile(Profile profile, SportWithSkillLevel sport)
        {
            var profileNode = _graphClient.ProfileWithId(profile.ProfileId).Single();

            CreateSingleSportRelationship(profileNode.Reference, sport);

            return true;
        }

        public bool AddLocationToProfile(Profile profile, Location location)
        {
            var profileNode = _graphClient.ProfileWithId(profile.ProfileId).Single();

            CreateSingleLocationRelationship(profileNode.Reference, location);

            return true;
        }

        public bool AddFriendToProfile(string profileId, string friendId)
        {

            var profileNode = _graphClient.ProfileWithId(profileId).Single();
            var friendNode = _graphClient.ProfileWithId(friendId).Single();

            CreateFriendRelationship(profileNode, friendNode);

            return true;
        }

        public IList<Profile> FindAllByName(string name)
        {
            var profileNodes = _graphClient.Accounts().Profiles().ToList();

            profileNodes = profileNodes.Where(x => x.Data.ProfileId.Contains(name)).ToList();

            var profiles = new List<Profile>();

            profileNodes.ForEach(x => profiles.Add(_profileMapper.Map(x)));

            return profiles;
        }

        public IList<Profile> FindAllBySport(string sport)
        {
            var sportNode = _graphClient.SportWithName(sport);

            var profileNodes = sportNode.InE(RelationsTypes.ProfileSport.ToString()).OutV<Profile>().ToList();

            var profiles = new List<Profile>();
            profileNodes.ForEach(node => profiles.Add(_profileMapper.Map(node)));

            return profiles;
        }

        public IList<Profile> FindAllByLocation(string location)
        {
            var locationNode = _graphClient.LocationWithName(location);

            var profileNodes = locationNode.InE(RelationsTypes.ProfileLocation.ToString()).OutV<Profile>().ToList();

            var profiles = new List<Profile>();
            profileNodes.ForEach(node => profiles.Add(_profileMapper.Map(node)));

            return profiles;
        }

        public IList<Profile> GetByAccount(string accountId)
        {
            var profileNodes = _graphClient.AccountWithId(accountId).Profiles().ToList();

            return profileNodes.Select(node => _profileMapper.Map(node)).ToList();
        }

        public Dictionary<string, string> GetFriendIdAndNameListForProfile(string profileId)
        {
            var friend1 = TestData.MockProfile2();
            var friend2 = TestData.MockProfile3();
            return new Dictionary<string, string> { { friend1.ProfileId, friend1.ProfileId }, { friend2.ProfileId, friend2.ProfileId } };
        }

        public bool ProfileExistsWithName(string profileName)
        {
            var nodes = _graphClient.ProfileWithId(profileName);

            return nodes.Any();
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
            _graphClient.CreateRelationship(profileNode.Reference, new FriendRelationship(friendNode.Reference));
        }

        private void CreateSingleLocationRelationship(NodeReference<Profile> profileNode, Location location)
        {
            var locNode = _graphClient.LocationWithName(location.Name).FirstOrDefault();
            if (locNode == null) return;
            _graphClient.CreateRelationship(profileNode, new ProfileToLocationRelationship(locNode.Reference));
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
            var sportNode = _graphClient.SportWithName(sport.Name).FirstOrDefault();
            if (sportNode == null) return;
            var profileToSportRelationship = new ProfileToSportRelationship(sportNode.Reference, sport.SkillLevel);
            _graphClient.CreateRelationship(profileNode,
                                            profileToSportRelationship);
        }
    }
}