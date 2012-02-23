﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class ProfileRepositoryTests
    {
        private const string AccountId = "Simtay111@gmail.com";
        private readonly SampleDataBuilder _data = new SampleDataBuilder();
        private ProfileRepository _repo;
        private const string ProfileId = "MyProfile";
        private const string Profile1Id = "Simtay111";

        [SetUp]
        public void SetUp()
        {
            _data.DeleteAllData();
            _repo = new ProfileRepository();
        }

        [Test]
        public void CanGetByAccount()
        {
            _data.CreateAccount1();
            _data.CreateProfileForAccount1();

            const string accountId = AccountId;
            var profiles = _repo.GetByAccount(accountId);

            Assert.That(profiles.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanSaveProfiles()
        {
            _data.CreateAccount1();
            _data.CreateLocationBend();
            _data.CreateSoccerSport();
            var profile = new Profile
                              {
                                  AccountId = AccountId,
                                  ProfileId = Profile1Id,
                                  Locations = new List<Location> { new Location("Bend") },
                                  SportsPlayed = new List<SportWithSkillLevel>
                                                     { new SportWithSkillLevel
                                                           {
                                                            Name = "Soccer",
                                                            SkillLevel = new SkillLevel(1)
                                                        }
                                                     }
                              };

            var wasSuccessful = _repo.Save(profile);

            var returnedProfiles = _repo.GetByAccount(profile.AccountId);
            Assert.True(wasSuccessful);
            Assert.That(returnedProfiles[0].ProfileId, Is.EqualTo(profile.ProfileId));
            Assert.That(returnedProfiles[0].Locations[0].Name, Is.EqualTo(profile.Locations[0].Name));
            Assert.That(returnedProfiles[0].SportsPlayed[0].Name, Is.EqualTo(profile.SportsPlayed[0].Name));
            Assert.That(returnedProfiles[0].SportsPlayed[0].SkillLevel.Level, Is.EqualTo(profile.SportsPlayed[0].SkillLevel.Level));
        }

        [Test]
        public void DoesNotThrowOrAddToLocationsIfLocationDoesNotExist()
        {
            _data.CreateAccount1();
            _data.CreateLocationBend();
            var profile = new Profile
            {
                AccountId = AccountId,
                ProfileId = Profile1Id,
                Locations = new List<Location> { new Location("Bend"), new Location("NonExisten") },
            };

            _repo.Save(profile);

            var returnedProfile = _repo.GetByAccount(profile.AccountId).First();
            Assert.That(returnedProfile.Locations[0].Name, Is.EqualTo(profile.Locations[0].Name));
            Assert.That(returnedProfile.Locations.Count, Is.EqualTo(1));
        }

        [Test]
        public void CanCheckIfAProfileAlreadyExistsWithProfileName()
        {
            _data.CreateAccount1();
            _data.CreateProfileForAccount1();

            Assert.True(_repo.ProfileExistsWithName(Profile1Id));
            Assert.False(_repo.ProfileExistsWithName(ProfileId));
        }

        [Test]
        public void CanAddSportsToProfile()
        {
            _data.CreateAccount1();
            _data.CreateSoccerSport();
            _data.CreateBasketballSport();
            var profile = _data.CreateProfileForAccount1();
            const string sport = "Soccer";

            _repo.AddSportToProfile(profile, new SportWithSkillLevel { Name = sport });

            var updatedProfile = _repo.GetByProfileId(profile.ProfileId);
            Assert.That(updatedProfile.SportsPlayed[1].Name, Is.EqualTo(sport));
        }

        [Test]
        public void CanGetByProfileId()
        {
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();

            var updatedProfile = _repo.GetByProfileId(profile.ProfileId);

            Assert.That(profile.ProfileId, Is.EqualTo(updatedProfile.ProfileId));
            Assert.That(profile.SportsPlayed.Count, Is.EqualTo(1));
            Assert.That(profile.Locations[0].Name, Is.EqualTo(profile.Locations[0].Name));
        }

        [Test]
        public void CanAddLocationsToProfile()
        {
            _data.CreateAccount1();
            _data.CreateSoccerSport();
            _data.CreateLocationBend();
            _data.CreateLocationPortland();
            var profile = _data.CreateProfileForAccount1();
            const string location = "Portland";

            _repo.AddLocationToProfile(profile, new Location { Name = location });

            var updatedProfile = _repo.GetByProfileId(profile.ProfileId);
            Assert.That(updatedProfile.Locations[1].Name, Is.EqualTo(location));
        }

        [Test]
        public void CanAddFriendsToProfile()
        {
            _data.CreateAccount1();
            var myProfile = _data.CreateProfileForAccount1();

            _data.CreateAccount2();
            var friendProf = _data.CreateProfileForAccount2();

            _repo.AddFriendToProfile(myProfile.ProfileId, friendProf.ProfileId);

            var updatedMyProfile = _repo.GetByProfileId(myProfile.ProfileId);
            var updatedFriendProf = _repo.GetByProfileId(friendProf.ProfileId);

            Assert.That(updatedFriendProf.FriendsIds.Count, Is.EqualTo(0));
            Assert.That(updatedMyProfile.FriendsIds.Count, Is.EqualTo(1));

            Assert.That(updatedMyProfile.FriendsIds[0], Is.EqualTo(updatedFriendProf.ProfileId));
        }

        [Test]
        public void CanFindAllByName()
        {
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();
            _data.CreateAccount2();
            var profile2 = _data.CreateProfileForAccount2();

            var profiles = _repo.FindAllByName("i");

            Assert.That(profiles.Count, Is.EqualTo(2));
            Assert.That(profiles[0].ProfileId, Is.EqualTo(profile.ProfileId));
            Assert.That(profiles[1].ProfileId, Is.EqualTo(profile2.ProfileId));
        }

        [Test]
        public void CanFindAllByNameExcludesProfileIdsWithoutMatches()
        {
            _data.CreateAccount1();
            var profile = _data.CreateProfileForAccount1();
            _data.CreateAccount2();
            _data.CreateProfileForAccount2();

            var profiles = _repo.FindAllByName("S");

            Assert.That(profiles.Count, Is.EqualTo(1));
            Assert.That(profiles[0].ProfileId, Is.EqualTo(profile.ProfileId));
        }

        [Test]
        public void CanFindAllBySports()
        {
            _data.CreateSoccerSport();
            _data.CreateBasketballSport();
            _data.CreateAccount1();
            var profile1 = _data.CreateProfileForAccount1();
            _data.CreateAccount2();
            var profile2 = _data.CreateProfileForAccount2();

            var soccerProfiles = _repo.FindAllBySport("Soccer");
            var basketballProfiles = _repo.FindAllBySport("Basketball");

            Assert.That(soccerProfiles.Count, Is.EqualTo(1));
            Assert.That(basketballProfiles.Count, Is.EqualTo(2));
            Assert.That(soccerProfiles[0].ProfileId, Is.EqualTo(profile2.ProfileId));
            Assert.That(basketballProfiles[0].ProfileId, Is.EqualTo(profile1.ProfileId));
            Assert.That(basketballProfiles[1].ProfileId, Is.EqualTo(profile2.ProfileId));
        }

        [Test]
        public void CanFindAllByLocation()
        {
            _data.CreateLocationBend();
            _data.CreateLocationPortland();
            _data.CreateAccount1();
            _data.CreateAccount2();
            var profile1 = _data.CreateProfileForAccount1();
            var profile2 = _data.CreateProfileForAccount2();

            var bendProfiles = _repo.FindAllByLocation("Bend");
            var portlandProfiles = _repo.FindAllByLocation("Portland");

            Assert.That(portlandProfiles.Count, Is.EqualTo(1));
            Assert.That(bendProfiles.Count, Is.EqualTo(2));
            Assert.That(portlandProfiles[0].ProfileId, Is.EqualTo(profile2.ProfileId));
            Assert.That(bendProfiles[0].ProfileId, Is.EqualTo(profile1.ProfileId));
            Assert.That(bendProfiles[1].ProfileId, Is.EqualTo(profile2.ProfileId));
        }
    }
}