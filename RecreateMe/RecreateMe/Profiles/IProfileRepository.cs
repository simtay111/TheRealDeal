
using System.Collections.Generic;

namespace RecreateMe.Profiles
{
    public interface IProfileRepository
    {
        Profile GetByProfileId(string profileId);
        bool SaveOrUpdate(Profile profile);
        IList<Profile> FindAllByName(string name);
        IList<Profile> FindAllBySport(string sport);
        IList<Profile> FindAllByLocation(string location);
        IList<Profile> GetByAccount(string accountName);
        Dictionary<string, Name> GetFriendIdAndNameListForProfile(string profileId);
    }
}