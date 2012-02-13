using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Profiles.Handlers
{
    [TestFixture]
    public class AddSportToProfileRequestHandlerTests
    {
        private Mock<IProfileRepository> _mockIProfileUpdater;
        private Mock<ISportRepository> _mockSportRepo;
        private AddSportToProfileRequest _request;
        private Profile _profile;
        private bool _profileWasSavedSuccessfully;

        [SetUp]
        public void SetUp()
        {
            _profileWasSavedSuccessfully = false;
        }

        [Test]
        public void CanAddSportToProfileViaUniqueIdOfPerson()
        {
            var skillLevel = 5;
            var uniqueId = "MyId";
            _request = new AddSportToProfileRequest()
                           {
                               SkillLevel = skillLevel,
                               Sport = "Soccer",
                               UniqueId = uniqueId
                           };
            _profile = new Profile();

            SetUpMockProfileUpdaterAndSportRepo(uniqueId);
            _mockIProfileUpdater.Setup(x => x.AddSportToProfile(It.Is<Profile>(d => d == _profile), It.IsAny<Sport>())).
                Callback(() => _profileWasSavedSuccessfully = true);

            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);
            var response = handler.Handle(_request);

            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.AreEqual(_profile.SportsPlayed.Count, 1);
            Assert.True(_profileWasSavedSuccessfully);
        }

        [Test]
        public void CreatesADefaultSkillLevelIfNoneIsSpecified()
        {
            var uniqueId = "MyId";
            _request = new AddSportToProfileRequest()
            {
                Sport = "Soccer",
                UniqueId = uniqueId
            };
            _profile = new Profile();

            SetUpMockProfileUpdaterAndSportRepo(uniqueId);


            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);
            handler.Handle(_request);
            Assert.AreEqual(_profile.SportsPlayed[0].SkillLevel.Level, Constants.DefaultSkillLevel);
        }
        
        [Test]
        public void ThrowsExceptionWhenThereIsNoSportSpecified()
        {
            var request = new AddSportToProfileRequest();
            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);

            var response = handler.Handle(request);
            
            Assert.That((object) response.Status, Is.EqualTo(ResponseCodes.SportNotSpecified));
        }

        private void SetUpMockProfileUpdaterAndSportRepo(string uniqueId)
        {
            _mockIProfileUpdater = new Mock<IProfileRepository>();
            _mockIProfileUpdater.Setup(x => x.GetByProfileId(It.Is<string>(d => d == uniqueId))).Returns(_profile);
            _mockSportRepo = new Mock<ISportRepository>();
            _mockSportRepo.Setup(x => x.FindByName(It.Is<string>(d => d == _request.Sport))).Returns(new Sport());
        }
    }
}