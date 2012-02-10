using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Friends.Handlers;
using RecreateMe.Profiles;
using RecreateMeSql;

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
            repo.Setup(x => x.GetFriendIdAndNameListForProfile(profile.ProfileId)).
                Returns(new Dictionary<string, string>()
                            {{friend1.ProfileId, friend1.ProfileId}, {friend2.ProfileId, friend2.ProfileId}});
            
            var request = new GetFriendsRequestHandlerRequest()
                              {
                                  ProfileId = profile.ProfileId
                              };

            var handler = new GetFriendsRequestHandler(repo.Object);

            var response = handler.Handle(request);

            Assert.That(response.FriendsNamesAndIds.Count, Is.EqualTo(2));
            Assert.That(response.FriendsNamesAndIds[friend1.ProfileId], Is.EqualTo(friend1.ProfileId));
            Assert.That(response.FriendsNamesAndIds[friend2.ProfileId], Is.EqualTo(friend2.ProfileId));
        }
         
    }
}