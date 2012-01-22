
using System.Collections.Generic;

namespace RecreateMe.Profiles
{
    public interface IProfileRepository
    {
        Profile GetByUniqueId(string uniqueId);
        bool SaveOrUpdate(Profile profile);
        IList<Profile> FindAllByName(string name);
        IList<Profile> FindByLocation(string locationName);
        IList<Profile> FindAllBySport(string sport);
        IList<Profile> FindAllByLocation(string location);
        IList<Profile> GetByAccount(string accountName);
    }
}