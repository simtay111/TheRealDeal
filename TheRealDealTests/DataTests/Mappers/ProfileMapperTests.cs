using System.Linq;
using Neo4jClient.Gremlin;
using NUnit.Framework;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMeSql.Connection;
using RecreateMeSql.Mappers;
using RecreateMeSql.Relationships;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Mappers
{
    [TestFixture]
    [Category("Integration")]
    public class ProfileMapperTests
    {
        readonly SampleDataBuilder _data = new SampleDataBuilder(); 

         [Test]
         public void CanMapAProfile()
         {
             _data.DeleteAllData();
             _data.CreateBasketballSport();
             _data.CreateLocationBend();
             _data.CreateAccount1();
             _data.CreateAccount2();
             var profile = _data.CreateProfileForAccount1();
             var profile2 = _data.CreateProfileForAccount2();
             _data.CreateFriendshipForProfile1And2();

             var profileMapper = new ProfileMapper();

             var gc = GraphClientFactory.Create();

             var accountNode = gc.RootNode.OutE(RelationsTypes.Account.ToString()).InV<Account>(n => n.AccountName == profile.AccountId);
             var profileNode = accountNode.OutE(RelationsTypes.HasProfile.ToString()).InV<Profile>().ToList().First();

             var finalizedProfile = profileMapper.Map(profileNode);

             Assert.That(finalizedProfile.ProfileId, Is.EqualTo(profile.ProfileId));
             Assert.That(finalizedProfile.Locations.Count, Is.EqualTo(1));
             Assert.That(finalizedProfile.Locations[0].Name, Is.EqualTo(profile.Locations[0].Name));
             Assert.That(finalizedProfile.SportsPlayed[0].Name, Is.EqualTo(profile.SportsPlayed[0].Name));
             Assert.That(finalizedProfile.SportsPlayed[0].SkillLevel.Level, Is.EqualTo(profile.SportsPlayed[0].SkillLevel.Level));
             Assert.That(finalizedProfile.FriendsIds[0], Is.EqualTo(profile2.ProfileId));
         }
    }
}