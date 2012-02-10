using NUnit.Framework;
using Neo4jClient;
using RecreateMe.Profiles;
using RecreateMeSql.Mappers;
using TheRealDealTests.DomainTests;

namespace TheRealDealTests.DataTests.Mappers
{
    [TestFixture]
    public class ProfileMapperTests
    {
         [Test]
         public void CanMapAProfile()
         {
             var profile = TestData.MockProfile1();
             var nodeRef = new NodeReference<Profile>(1);

             var profileNode = new Node<Profile>(profile, nodeRef);

             var profileMapper = new ProfileMapper();

             var finalizedProfile = profileMapper.Map(profileNode);

             Assert.That(finalizedProfile.ProfileId, Is.EqualTo(profile.ProfileId));
         }
    }
}