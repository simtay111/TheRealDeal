using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Relationships;

namespace RecreateMeSql.Mappers
{
    public class ProfileMapper
    {
        public Profile Map(Node<Profile> profileNode)
        {
            var profile = CreateProfileAndMapId(profileNode);

            MapLocations(profileNode, profile);
            MapSports(profileNode, profile);
            MapFriends(profileNode, profile);

            return profile;
        }

        private void MapFriends(Node<Profile> profileNode, Profile profile)
        {
            var friendNodes = profileNode.Friends().ToList();
            friendNodes.ForEach(x => profile.FriendsIds.Add(x.Data.ProfileId));
        }

        private void MapSports(Node<Profile> profileNode, Profile profile)
        {
            var sportEdges = profileNode.OutE<SkillLevel>(RelationsTypes.ProfileSport);
            var sportEdgesList = profileNode.OutE<SkillLevel>(RelationsTypes.ProfileSport).ToList();
            var sportNodes = sportEdges.InV<Sport>().ToList();

            for(int i = 0; i < sportEdges.Count(); i++)
            {
                var skillLevel = sportEdgesList[i].Data.Level;
                 profile.SportsPlayed.Add(new SportWithSkillLevel {Name = sportNodes[i].Data.Name, SkillLevel = new SkillLevel(skillLevel)});
            }
        }

        private Profile CreateProfileAndMapId(Node<Profile> profileNode)
        {
            return new Profile { ProfileId = profileNode.Data.ProfileId };
        }

        private void MapLocations(Node<Profile> profileNode, Profile profile)
        {
            var locations = profileNode.OutE(RelationsTypes.ProfileLocation)
                .InV<Location>();

            foreach (var createdLoc in locations.Select(location => new Location(location.Data.Name)))
            {
                profile.Locations.Add(createdLoc);
            }
        }
    }
}