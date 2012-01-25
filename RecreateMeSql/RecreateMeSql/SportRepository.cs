using System;
using System.Collections.Generic;
using RecreateMe.Sports;

namespace RecreateMeSql
{
    public class SportRepository : ISportRepository
    {
        public Sport FindByName(string name)
        {
            return TestData.CreateSoccerGame();
        }

        public IList<string> GetNamesOfAllSports()
        {
            var listOfSports = new List<string>
                                   {
                                       TestData.CreateBasketballGame().Name,
                                       TestData.CreateSoccerGame().Name,
                                   };

            return listOfSports;
        }
    }
}