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

            return profile;
        }
        
        private void MapSports(Node<Profile> profileNode, Profile profile)
        {
            var sportEdges = profileNode.OutE<SkillLevel>(RelationsTypes.ProfileSport.ToString());
            var sportEdgesList = profileNode.OutE<SkillLevel>(RelationsTypes.ProfileSport.ToString()).ToList();
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
            var locations = profileNode.OutE(RelationsTypes.ProfileLocation.ToString())
                .InV<Location>();

            foreach (var createdLoc in locations.Select(location => new Location(location.Data.Name)))
            {
                profile.Locations.Add(createdLoc);
            }
        }
    }
}