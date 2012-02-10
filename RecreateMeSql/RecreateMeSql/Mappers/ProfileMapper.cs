using Neo4jClient;
using RecreateMe.Profiles;

namespace RecreateMeSql.Mappers
{
    public class ProfileMapper
    {
        public Profile Map(Node<Profile> profileNode)
        {
            var profile = new Profile()
                              {

                                  ProfileId = profileNode.Data.ProfileId,
                              };

            return profile;
        }
    }
}