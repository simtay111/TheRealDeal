
using System.Collections.Generic;
using RecreateMe.Locales;
using RecreateMe.Sports;

namespace RecreateMe.Profiles
{
    public interface IProfileRepository
    {
        Profile GetByProfileId(string profileId);
        bool SaveOrUpdate(Profile profile);
        bool AddSportToProfile(Profile profile, Sport sport);
        bool AddLocationToProfile(Profile profile, Location location);
        bool AddFriendToProfile(Profile profile, Profile friend);
        IList<Profile> FindAllByName(string name);
        IList<Profile> FindAllBySport(string sport);
        IList<Profile> FindAllByLocation(string location);
        IList<Profile> GetByAccount(string accountName);
        Dictionary<string, Name> GetFriendIdAndNameListForProfile(string profileId);
    }
}