using System.Linq;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMeSql.Relationships;

namespace RecreateMeSql.Mappers
{
    public class ProfileMapper
    {
        public Profile Map(Node<Profile> profileNode)
        {
            var profile = CreateProfileAndMapId(profileNode);

            MapLocations(profileNode, profile);

            return profile;
        }

        private Profile CreateProfileAndMapId(Node<Profile> profileNode)
        {
            return new Profile { ProfileId = profileNode.Data.ProfileId };
        }

        private void MapLocations(Node<Profile> profileNode, Profile profile)
        {
            var locations = profileNode.OutE(RelationsTypes.ProfileLocation.ToString())
                .InV<Location>();

            foreach (var createdLoc in locations.Select(location => new Location(location.Data.Name)))
            {
                profile.Locations.Add(createdLoc);
            }
        }
    }
}