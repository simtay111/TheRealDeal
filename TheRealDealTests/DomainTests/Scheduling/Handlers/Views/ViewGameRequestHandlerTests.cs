using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RecreateMe;
using RecreateMe.Profiles;
using RecreateMe.Scheduling;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers.Views;
using RecreateMe.Sports;

namespace TheRealDealTests.DomainTests.Scheduling.Handlers.Views
{
    [TestFixture]
    public class ViewGameRequestHandlerTests
    {
        private Mock<IProfileRepository> _profileRepo;
        private Mock<IPickUpGameRepository> _pickupGameRepo;

        [SetUp]
        public void SetUp()
        {
            _profileRepo = new Mock<IProfileRepository>();
            _pickupGameRepo = new Mock<IPickUpGameRepository>();
        }

        [Test]
        public void CanGetProfilesForGame()
        {
            var game = new PickUpGame();
            const string sportName = "Soccer";
            game.Sport = sportName;
            const string profileId = "Larry";
            const string profileId2 = "Bob";
            var profile1 = new Profile {ProfileId = profileId,  SportsPlayed = new List<SportWithSkillLevel> {new SportWithSkillLevel(){Name = sportName, SkillLevel = new SkillLevel(1)}}};
            var profile2 = new Profile {ProfileId = profileId2, SportsPlayed = new List<SportWithSkillLevel> {new SportWithSkillLevel(){Name = sportName, SkillLevel = new SkillLevel(3)}}};
            var profiles = new List<Profile> {profile1, profile2};
            _profileRepo.Setup(x => x.GetProfilesInGame(game.Id)).Returns(profiles);
            _pickupGameRepo.Setup(x => x.GetPickUpGameById(game.Id)).Returns(game);

            var request = new ViewGameRequest() {GameId = game.Id};

            var handler = new ViewGameRequestHandler(_profileRepo.Object, _pickupGameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Game, Is.EqualTo(game));
            Assert.That(response.ProfilesAndSkillLevels.Count, Is.EqualTo(2));
            Assert.That(response.ProfilesAndSkillLevels[profileId], Is.EqualTo(1));
            Assert.That(response.ProfilesAndSkillLevels[profileId2], Is.EqualTo(3));
        }

        [Test]
        public void CanDoSomething()
        {
            var game = new PickUpGame();
            const string sportName = "Soccer";
            game.Sport = sportName;
            const string profileId = "Larry";
            var profile1 = new Profile { ProfileId = profileId, SportsPlayed = new List<SportWithSkillLevel>() };
            var profiles = new List<Profile> { profile1};
            _profileRepo.Setup(x => x.GetProfilesInGame(game.Id)).Returns(profiles);
            _pickupGameRepo.Setup(x => x.GetPickUpGameById(game.Id)).Returns(game);

            var request = new ViewGameRequest() { GameId = game.Id };

            var handler = new ViewGameRequestHandler(_profileRepo.Object, _pickupGameRepo.Object);

            var response = handler.Handle(request);

            Assert.That(response.Game, Is.EqualTo(game));
            Assert.That(response.ProfilesAndSkillLevels.Count, Is.EqualTo(1));
            Assert.That(response.ProfilesAndSkillLevels[profileId], Is.EqualTo(Constants.DefaultSkillLevel));
        }
    }
}
