using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;

namespace TheRealDealTests.DomainTests.Profiles.Handlers
{
    [TestFixture]
    public class GetProfilesForAccountRequestHandlerTests
    {
        [Test]
        public void CanRetrieveAListOfProfilesAssociatedToAnAccount()
        {
            const string accountName = "User1";
            var request = new GetProfilesForAccountRequest() { Account = accountName };
            var mockProfileRepo = new Mock<IProfileRepository>();
            mockProfileRepo.Setup(x => x.GetByAccount(accountName)).Returns(new List<Profile>() 
                { new Profile(), new Profile() });
            var handler = new GetProfilesForAccountRequestHandle(mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Profiles.Count, Is.EqualTo(2));
        }

        [Test]
        public void ReturnsAnEmptyListIfNoProfilesExistForAccount()
        {
            const string accountName = "User1";
            var request = new GetProfilesForAccountRequest() { Account = accountName };
            var mockProfileRepo = new Mock<IProfileRepository>();
            mockProfileRepo.Setup(x => x.GetByAccount(accountName)).Returns(new List<Profile>());
            var handler = new GetProfilesForAccountRequestHandle(mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Profiles.Count, Is.EqualTo(0));
        }
         
    }
}