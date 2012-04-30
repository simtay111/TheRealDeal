
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
        bool AddFriendToProfile(string profileId, string friendId);
        IList<Profile> FindAllByName(string name);
        IList<Profile> FindAllBySport(string sport);
        IList<Profile> FindAllByLocation(string location);
        IList<Profile> GetByAccount(string accountId);
        IList<string> GetFriendsProfileNameList(string profileId);
        bool ProfileExistsWithName(string profileName);
        void RemoveSportFromProfile(string profileId, string sportName);
        void RemoveLocationFromProfile(string profileId, string locationName);
    }
}