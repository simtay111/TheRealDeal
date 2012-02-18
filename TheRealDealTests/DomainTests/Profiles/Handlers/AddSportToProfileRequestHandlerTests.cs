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
            const int skillLevel = 5;
            const string uniqueId = "MyId";
            _request = new AddSportToProfileRequest
                           {
                               SkillLevel = skillLevel,
                               Sport = "Soccer",
                               UniqueId = uniqueId
                           };
            _profile = new Profile();

            SetUpMockProfileUpdaterAndSportRepo(uniqueId);
            _mockIProfileUpdater.Setup(x => x.AddSportToProfile(It.Is<Profile>(d => d == _profile), It.IsAny<SportWithSkillLevel>())).
                Callback(() => _profileWasSavedSuccessfully = true);

            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);
            var response = handler.Handle(_request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.Success));
            Assert.True(_profileWasSavedSuccessfully);
        }

        [Test]
        public void WillNotAddSportIfItAlreadyIsInProfile()
        {
            _profile = TestData.MockProfile1();
            _request = new AddSportToProfileRequest
                           {
                               SkillLevel = 1,
                               Sport = "Soccer",
                               UniqueId = _profile.ProfileId
                           };
            SetUpMockProfileUpdaterAndSportRepo(_profile.ProfileId);
            _mockSportRepo.Setup(x => x.FindByName(_request.Sport)).Returns(new Sport {Name = "Soccer"});
            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);

            var response = handler.Handle(_request);

            Assert.That(response.Status, Is.EqualTo(ResponseCodes.SportAlreadyPlayed));
        }

        [Test]
        public void CreatesADefaultSkillLevelIfNoneIsSpecified()
        {
            const string uniqueId = "MyId";
            _request = new AddSportToProfileRequest
                           {
                Sport = "Soccer",
                UniqueId = uniqueId
            };
            _profile = new Profile();
            SetUpMockProfileUpdaterAndSportRepo(uniqueId);
            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);

            handler.Handle(_request);

            _mockIProfileUpdater.Verify(x => x.AddSportToProfile(_profile,
                It.Is<SportWithSkillLevel>(n => n.SkillLevel.Level ==Constants.DefaultSkillLevel)));
        }
        
        [Test]
        public void ThrowsExceptionWhenThereIsNoSportSpecified()
        {
            var request = new AddSportToProfileRequest();
            var handler = new AddSportToProfileRequestHandler(_mockIProfileUpdater.Object, _mockSportRepo.Object);

            var response = handler.Handle(request);
            
            Assert.That(response.Status, Is.EqualTo(ResponseCodes.SportNotSpecified));
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