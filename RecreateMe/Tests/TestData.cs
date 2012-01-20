using System.Collections.Generic;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;

namespace RecreateMe.Tests
{
    public static class TestData
    {
        private static Profile _profile1;
        private static Profile _profile2;
        private static Profile _profile3;
        private static Sport _soccer;
        private static Sport _basketball;
        private static Location _location1;
        private static Location _location2;

        public static IList<Profile> GetListOfMockedProfiles()
        {
            return new List<Profile>
                       {
                           MockProfile1(),
                           MockProfile2(),
                           MockProfile3()
                       };
        }

        public static Profile MockProfile1()
        {
            _profile1 = new Profile()
                           {
                               Name = new Name("Profile", "One"),
                               SportsPlayed = { CreateSoccerWithSkillLevel() },
                               UniqueId = "Prof1",
                               Locations = {CreateLocation1()}
                           };
            return _profile1;
        }

        public static Profile MockProfile2()
        {
            _profile2 = new Profile()
                           {
                               Name = new Name("Profile", "Two"),
                               SportsPlayed = { CreateSoccerWithSkillLevel() },
                               UniqueId = "Prof2",
                               Locations = {CreateLocation2()}
                           };
            return _profile2;
        }

        public static Profile MockProfile3()
        {
            _profile3 = new Profile()
                           {
                               Name = new Name("Profile", "Three"),
                               SportsPlayed = { CreateBasketballWithSkillLevel() },
                               UniqueId = "Prof3",
                               Locations = {CreateLocation3()}
                           };
            return _profile3;
        }

        public static SportWithSkillLevel CreateSoccerWithSkillLevel(){
            var sport = new SportWithSkillLevel()
                            {
                                Name = "Soccer",
                                SkillLevel = new SkillLevel(Constants.DefaultSkillLevel)
                            };
            return sport;
        }

        public static SportWithSkillLevel CreateBasketballWithSkillLevel(){
            var sport = new SportWithSkillLevel()
                            {
                                Name = "Basketball",
                                SkillLevel = new SkillLevel(Constants.DefaultSkillLevel)
                            };
            return sport;
        }

        public static Location CreateLocation3()
        {
            return new Location(123, "Else");
        }
        public static Location CreateLocation1()
        {
            return new Location(1234, "Somewhere");
        }
        public static Location CreateLocation2()
        {
            return new Location(1235, "Bend");
        }

        public static Sport CreateSoccerGame()
        {
            _soccer = new Sport()
                       {
                           Name = "Soccer"
                       };
            return _soccer;
        }
        public static Sport CreateBasketballGame()
        {
           _basketball = new Sport()
                       {
                           Name = "Basketball"
                       };
            return _basketball;
        }
        public static Location CreateLocationBend()
        {
            _location1 = new Location(1, "Bend");
            return _location1;
        }
        public static Location CreateLocationHamsterville()
        {
            _location2 = new Location(2, "Hamsterville");
            return _location2;
        }

        public static IList<Location> ListOfLocations()
        {
            return new List<Location>
                       {
                           CreateLocation1(),
                           CreateLocation2(),
                           CreateLocation3(),
                           CreateLocationBend(),
                           CreateLocationHamsterville()
                       };
        }


    }
}