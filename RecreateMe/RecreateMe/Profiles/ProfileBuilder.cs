using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public class ProfileBuilder : IProfileBuilder
    {
        private Profile _profile;
        public virtual Sport Sport { get; set; }
        public virtual string ProfileId { get; private set; }
        public SkillLevel LevelOfSkill { get; set; }
        public virtual Location Location { get; private set; }

        public virtual Profile Build()
        {
            HandleNullParameters();
            CreateProfileWithDataFromBuilder();

            return _profile;
        }

        private void CreateProfileWithDataFromBuilder()
        {
            _profile = new Profile
            {
                ProfileName = ProfileId
            };
            _profile.Locations.Add(Location);
            CreateSportAndSkillLevel();
        }

        private void CreateSportAndSkillLevel()
        {
            if (Sport != null)
            {
                var sportWithSkillLevel = new SportWithSkillLevel {Name = Sport.Name};
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

        public ProfileBuilder WithProfileId(string profileId)
        {
            ProfileId = profileId;
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