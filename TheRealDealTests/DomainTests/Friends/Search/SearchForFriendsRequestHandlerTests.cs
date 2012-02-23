using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Friends.Search;
using RecreateMe.Profiles;

namespace TheRealDealTests.DomainTests.Friends.Search
{
    [TestFixture]
    public class SearchForFriendsRequestHandlerTests
    {
        private Mock<IProfileRepository> _mockProfileRepo;
        private const string Name1 = "Billy";
        private const string Sport = "Soccer";
        private const string Location = "Bend";

        [SetUp]
        public void SetUp()
        {
            _mockProfileRepo = new Mock<IProfileRepository>();
        }

        [Test]
        public void DoesNotFindSelf()
        {
            var request = new SearchForFriendsRequest { ProfileName = Name1, Sport = "", Location = "", MyProfile = "MyProfile" };
            var profile = new Profile {ProfileId = request.MyProfile};
            var friendProfile = new Profile {ProfileId = Name1};
            var listOfProfiles = new List<Profile> {profile, friendProfile};

            var mockProfileRepo = new Mock<IProfileRepository>();
            mockProfileRepo.Setup(x => x.FindAllByName(request.ProfileName)).Returns(listOfProfiles);

            var handler = new SearchForFriendsRequestHandler(mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Results.Count, Is.EqualTo(1));
            Assert.That(response.Results[0], Is.SameAs(friendProfile));
        }

        [Test]
        public void CanSearchByName()
        {
            var request = new SearchForFriendsRequest {ProfileName = Name1, Sport = "", Location = ""};

            SetupProfileRepoToReturnThreeProfilesForName();

            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Results.Count, Is.EqualTo(3));
        }

        [Test]
        public void CanSearchByNameAndSport()
        {
            var request = new SearchForFriendsRequest {ProfileName = Name1, Sport = Sport, Location = ""};

            SetupProfileRepoToReturnThreeProfilesForName();

            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Results.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanSearchByNameLocationAndSport()
        {
            var request = new SearchForFriendsRequest {ProfileName = Name1, Sport = Sport, Location = Location};

            SetupProfileRepoToReturnThreeProfilesForName();

            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Results.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanSearchBySportOnly()
        {
            var request = new SearchForFriendsRequest { ProfileName = "", Sport = Sport, Location = "" };

            SetupProfileRepoForSports();

            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Results.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanSearchByLocationOnly()
        {
            var request = new SearchForFriendsRequest { ProfileName = "", Sport = "", Location = Location };

            SetupProfileRepoToReturnTForLocationQuery();

            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Results.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanSearchByLocationAndSportOnly()
        {
            var request = new SearchForFriendsRequest { ProfileName = "", Sport = Sport, Location = Location };

            SetupProfileRepoToReturnTForLocationQuery();
            SetupProfileRepoForSports();

            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Results.Count, Is.EqualTo(1));
        }

        [Test]
        public void ReturnsBadResponseCodeIfNoFieldsWereSpecified()
        {
            var request = new SearchForFriendsRequest { ProfileName = "", Sport = "", Location = "" };
            var handler = new SearchForFriendsRequestHandler(_mockProfileRepo.Object);

            var response = handler.Handle(request);

            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.NoCriteriaSpecified));
        }

        private void SetupProfileRepoToReturnThreeProfilesForName()
        {
            var friendResults = TestData.GetListOfMockedProfiles();
            _mockProfileRepo.Setup(x => x.FindAllByName(Name1)).Returns(friendResults);
        }

        private void SetupProfileRepoForSports()
        {
            var friendsList = TestData.GetListOfMockedProfiles().
            Where(x => (x.SportsPlayed.Where(y => y.Name == Sport)).Count() > 0).ToList();
            _mockProfileRepo.Setup(x => x.FindAllBySport(Sport)).Returns(friendsList);
        }

        private void SetupProfileRepoToReturnTForLocationQuery()
        {
            var friendsList = TestData.GetListOfMockedProfiles().
            Where(x => (x.Locations.Where(y => y.Name == Location)).Count() > 0).ToList();
            _mockProfileRepo.Setup(x => x.FindAllByLocation(Location)).Returns(friendsList);
        }
    }
}