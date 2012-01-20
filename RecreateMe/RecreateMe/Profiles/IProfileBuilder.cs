using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public interface IProfileBuilder
    {
        Profile Build();
        ProfileBuilder WithName(Name name);
        ProfileBuilder WithLocation(Location location);
        ProfileBuilder WithSport(Sport sport);
        ProfileBuilder WithSkillLevel(SkillLevel skillLevel);
    }
}