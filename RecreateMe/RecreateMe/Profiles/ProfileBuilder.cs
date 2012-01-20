using System;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public class ProfileBuilder : IProfileBuilder
    {
        private Profile _profile;
        public virtual Sport Sport { get; set; }
        public virtual Name Name { get; private set; }
        public SkillLevel LevelOfSkill { get; set; }
        public virtual Location Location { get; private set; }

        public virtual Profile Build()
        {
            HandleNullParameters();
            CreateProfileWithDataFromBuilder();
            _profile.UniqueId = Guid.NewGuid().ToString();

            return _profile;
        }

        private void CreateProfileWithDataFromBuilder()
        {
            _profile = new Profile
            {
                Name = Name
            };
            _profile.Locations.Add(Location);
            CreateSportAndSkillLevel();
        }

        private void CreateSportAndSkillLevel()
        {
            if (Sport != null)
            {
                var sportWithSkillLevel = new SportWithSkillLevel();
                CreateSkillLevel(sportWithSkillLevel);
                _profile.SportsPlayed.Add(sportWithSkillLevel);
            }
        }

        private void CreateSkillLevel(SportWithSkillLevel sportWithSkillLevel)
        {
            if (LevelOfSkill != null)
                sportWithSkillLevel.SkillLevel = LevelOfSkill;
        }

        private void HandleNullParameters()
        {
            if (Location == null)
            {
                Location = Location.Default;
            }
        }

        public ProfileBuilder WithName(Name name)
        {
            Name = name;
            return this;
        }

        public ProfileBuilder WithLocation(Location location)
        {
            Location = location;
            return this;
        }


        public ProfileBuilder WithSport(Sport sport)
        {
            Sport = sport;
            return this;
        }

        public ProfileBuilder WithSkillLevel(SkillLevel skillLevel)
        {
            LevelOfSkill = skillLevel;
            return this;
        }
    }
}