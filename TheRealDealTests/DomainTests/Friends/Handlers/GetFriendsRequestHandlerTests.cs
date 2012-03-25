using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Friends.Handlers;
using RecreateMe.Profiles;

namespace TheRealDealTests.DomainTests.Friends.Handlers
{
    [TestFixture]
    public class GetFriendsRequestHandlerTests
    {
        [Test]
        public void CanGetNameIdDictionaryForFriends()
        {
            var profile = TestData.MockProfile1();
            var friend1 = TestData.MockProfile2();
            var friend2 = TestData.MockProfile3();

            var repo = new Mock<IProfileRepository>();
            repo.Setup(x => x.GetFriendsProfileNameList(profile.ProfileId)).
                Returns(new List<string>
                            {friend1.ProfileId, friend2.ProfileId});
            
            var request = new GetFriendsRequestHandlerRequest()
                              {
                                  ProfileId = profile.ProfileId
                              };

            var handler = new GetFriendsRequestHandler(repo.Object);

            var response = handler.Handle(request);

            Assert.That(response.FriendsNamesAndIds.Count, Is.EqualTo(2));
            Assert.That(response.FriendsNamesAndIds[0], Is.EqualTo(friend1.ProfileId));
            Assert.That(response.FriendsNamesAndIds[1], Is.EqualTo(friend2.ProfileId));
        }
         
    }
}