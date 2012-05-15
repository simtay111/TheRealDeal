using System;
using System.Collections.Generic;
using NUnit.Framework;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class SuperDataBuilder : SampleDataBuilder
    {
        public List<string> ProfileIds = new List<string> { "Bilbo", "Gregory", "Jimbo", "Nancy", "Pickles" };
        public List<string> AccountIds = new List<string> { "Bilbo@Bilbo.com", "Gregory@Gregory.com", "Jimbo@Jimbo.com", "Nancy@Nancy.com", "Pickles@Pickles.com" };
        public List<string> SportIds = new List<string> { SoccerName, FootballName, Basketballname };
        public List<string> LocationIds = new List<string> { LocationPortland, LocationBendName };

        [Test]
        [Category("LargeData")]
        [Category("Integration")]
        public void BuildData()
        {
            CreateData();
            CreateExtraAccounts();
            CreateExtraProfiles();
            CreateExtraGames();
        }

        private void CreateExtraGames()
        {
            for (var k = 0; k < 100; k++)
            {
                var randomNumber = new Random(k);
                Console.WriteLine("Creating record: " + k);
                var game = new PickUpGame(DateTime.Now, new Sport(), new Location());
                game.MaxPlayers = 5;
                game.MinPlayers = 3;
                game.IsPrivate = true;
                game.Sport = SportIds[((int)(randomNumber.NextDouble() * SportIds.Count))];
                game.Location = LocationIds[((int)(randomNumber.NextDouble() * LocationIds.Count))];
                for (int i = 0; i < 5; i++)
                {
                    var profileId = ProfileIds[((int)(randomNumber.NextDouble() * ProfileIds.Count))];
                    if (game.PlayersIds.Contains(profileId))
                        continue;
                    game.AddPlayer(profileId);
                }
                game.Creator = Profile1Id;

                new PickUpGameRepository().SavePickUpGame(game);
            }
        }

        private void CreateExtraProfiles()
        {
            var profileRepo = new ProfileRepository();

            for (int i = 0; i < ProfileIds.Count; i++)
            {
                var profile = new Profile
                                  {
                                      AccountName = AccountIds[i],
                                      ProfileName = ProfileIds[i],
                                      Locations =
                                          new List<Location> { new Location(LocationBendName), new Location(LocationPortland) },
                                      SportsPlayed = new List<SportWithSkillLevel>
                                                         {
                                                             new SportWithSkillLevel
                                                                 {
                                                                     Name = SoccerName,
                                                                     SkillLevel = new SkillLevel(2)
                                                                 },
                                                             new SportWithSkillLevel
                                                                 {
                                                                     Name = Basketballname,
                                                                     SkillLevel = new SkillLevel(7)
                                                                 },
                                                             new SportWithSkillLevel
                                                                 {
                                                                     Name = FootballName,
                                                                     SkillLevel = new SkillLevel(5)
                                                                 }
                                                         }
                                  };
                profileRepo.Save(profile);
            }
        }

        private void CreateExtraAccounts()
        {
            var userRepo = new UserRepository();

            foreach (var accountid in AccountIds)
                userRepo.CreateUser(accountid, "password");
        }
    }
}