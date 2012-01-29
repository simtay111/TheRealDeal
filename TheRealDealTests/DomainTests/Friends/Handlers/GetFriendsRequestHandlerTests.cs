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
            repo.Setup(x => x.GetFriendIdAndNameListForProfile(profile.UniqueId)).
                Returns(new Dictionary<string, Name>()
                            {{friend1.UniqueId, friend1.Name}, {friend2.UniqueId, friend2.Name}});
            
            var request = new GetFriendsRequestHandlerRequest()
                              {
                                  ProfileId = profile.UniqueId
                              };

            var handler = new GetFriendsRequestHandler(repo.Object);

            var response = handler.Handle(request);

            Assert.That(response.FriendsNamesAndIds.Count, Is.EqualTo(2));
            Assert.That(response.FriendsNamesAndIds[friend1.UniqueId], Is.EqualTo(friend1.Name));
            Assert.That(response.FriendsNamesAndIds[friend2.UniqueId], Is.EqualTo(friend2.Name));
        }
         
    }
}