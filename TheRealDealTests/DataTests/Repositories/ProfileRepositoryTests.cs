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
                Locations = new List<Location>() { new Location("Bend"), new Location("NonExisten") },
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
    }
}