using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public interface IProfileBuilder
    {
        Profile Build();
        ProfileBuilder WithProfileId(string profileId);
        ProfileBuilder WithLocation(Location location);
        ProfileBuilder WithSport(Sport sport);
        ProfileBuilder WithSkillLevel(SkillLevel skillLevel);
    }
}