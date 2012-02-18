
using System.Collections.Generic;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public interface IProfileRepository
    {
        Profile GetByProfileId(string profileId);
        bool Save(Profile profile);
        bool AddSportToProfile(Profile profile, SportWithSkillLevel sport);
        bool AddLocationToProfile(Profile profile, Location location);
        bool AddFriendToProfile(Profile profile, Profile friend);
        IList<Profile> FindAllByName(string name);
        IList<Profile> FindAllBySport(string sport);
        IList<Profile> FindAllByLocation(string location);
        IList<Profile> GetByAccount(string accountId);
        Dictionary<string, string> GetFriendIdAndNameListForProfile(string profileId);
        bool ProfileExistsWithName(string profileName);
    }
}