using System.Collections.Generic;

namespace RecreateMe.Sports
{
    public interface ISportRepository
    {
        Sport FindByName(string name);
        IList<string> GetNamesOfAllSports();
        void CreateSport(string sportName);
    }
}