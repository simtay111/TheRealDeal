using System.Collections.Generic;
using System.Linq;
using RecreateMe;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMe.Teams;

namespace TheRealDealTests.DomainTests
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
            _profile1 = new Profile
                            {
                               ProfileName = "ProfileOne",
                               SportsPlayed = { CreateSoccerWithSkillLevel() },
                               Locations = {CreateLocation1()}
                           };
            return _profile1;
        }

        public static Profile MockProfile2()
        {
            _profile2 = new Profile
                            {
                               ProfileName = "ProfileTwo",
                               SportsPlayed = { CreateSoccerWithSkillLevel() },
                               Locations = {CreateLocation2()}
                           };
            return _profile2;
        }

        public static Profile MockProfile3()
        {
            _profile3 = new Profile
                            {
                               ProfileName = "ProfileThree",
                               SportsPlayed = { CreateBasketballWithSkillLevel() },
                               Locations = {CreateLocation3()}
                           };
            return _profile3;
        }

        public static SportWithSkillLevel CreateSoccerWithSkillLevel(){
            var sport = new SportWithSkillLevel
                            {
                                Name = "Soccer",
                                SkillLevel = new SkillLevel(Constants.DefaultSkillLevel)
                            };
            return sport;
        }

        public static SportWithSkillLevel CreateBasketballWithSkillLevel(){
            var sport = new SportWithSkillLevel
                            {
                                Name = "Basketball",
                                SkillLevel = new SkillLevel(Constants.DefaultSkillLevel)
                            };
            return sport;
        }

        public static Location CreateLocation3()
        {
            return new Location("Else");
        }
        public static Location CreateLocation1()
        {
            return new Location("Somewhere");
        }
        public static Location CreateLocation2()
        {
            return new Location("Bend");
        }

        public static Sport CreateSoccerGame()
        {
            _soccer = new Sport
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

        public static Team CreateTeam1()
        {
            return new Team
                       {
                           MaxSize = 6,
                           Name = "Super team",
                           PlayersIds = GetListOfMockedProfiles().Select(x => x.ProfileName).ToList()
                       };
        }

        public static Team CreateTeam2()
        {
            var profiles = GetListOfMockedProfiles();
            profiles.RemoveAt(0);

            return new Team()
                       {
                           MaxSize = 12,
                           Name = "Mega team",
                           PlayersIds = profiles.Select(x => x.ProfileName).ToList()
                       };
        }

        public static Location CreateLocationBend()
        {
            _location1 = new Location("Bend");
            return _location1;
        }
        public static Location CreateLocationHamsterville()
        {
            _location2 = new Location("Hamsterville");
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