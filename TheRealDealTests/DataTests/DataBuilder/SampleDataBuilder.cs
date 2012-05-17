using System;
using System.Collections.Generic;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Games;
using RecreateMe.Sports;
using RecreateMe.Teams;
using RecreateMeSql;
using RecreateMeSql.Repositories;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class SampleDataBuilder
    {
        public string TeamId2;
        public string TeamId1;
        public string TeamId3;
        public PickUpGame PickUpGame;
        public string GameWithTeamsId;
        public const string LocationPortland = "Portland";
        public const string Profile2Id = "Profile1";
        public const string SoccerName = "Soccer";
        public const string LocationBendName = "Bend";
        public const string TeamName1 = "Team1";
        public const string TeamName2 = "Team2";
        public const string TeamName3 = "Team3";
        public const string Profile1Id = "Simtay111";
        public const string FootballName = "Football";
        public const string Basketballname = "Basketball";

        public void CreateData()
        {
            SqlServerDataHelper.DeleteAllData();
            CreateAccounts();
            CreateLocations();
            CreateSports();
            CreateProfilesForAccounts();
            //CreateFriendship();
            //CreateTeams();
            CreateGames();
        }

        protected void CreateGames()
        {
            CreateGameWithProfile1();
            CreateGameWithProfile1AndProfile2();
            //CreateGameWithTeams1And2();
        }

        private void CreateTeams()
        {
            CreateTeam1();
            CreateTeam2();
            CreateTeam3();
        }

        private void CreateFriendship()
        {
            CreateFriendshipForProfile1And2();
        }

        private void CreateLocations()
        {
            CreateLocationBend();
            CreateLocationPortland();
        }

        private void CreateSports()
        {
            CreateSoccerSport();
            CreateBasketballSport();
            CreateFootballSport();
        }

        private void CreateProfilesForAccounts()
        {
            CreateProfileForAccount1();
            CreateProfileForAccount2();
        }

        public Profile CreateProfileForAccount2()
        {
            var profileRepo = new ProfileRepository();

            var profile = new Profile
                              {
                                  AccountName = "Cows@Moo.com",
                                  ProfileName = Profile2Id,
                                  Locations = new List<Location> { new Location(LocationBendName), new Location(LocationPortland) },
                                  SportsPlayed = new List<SportWithSkillLevel>
                                                     {new SportWithSkillLevel
                                                          {
                                                            Name = SoccerName,
                                                            SkillLevel = new SkillLevel(5)
                                                        },
                                                     new SportWithSkillLevel
                                                          {
                                                            Name = Basketballname,
                                                            SkillLevel = new SkillLevel(5)
                                                        }}
                              };
            profileRepo.Save(profile);
            return profile;
        }

        public Profile CreateProfileForAccount1()
        {
            var profileRepo = new ProfileRepository();

            var profile = new Profile
                              {
                                  AccountName = "Simtay111@Gmail.com",
                                  ProfileName = Profile1Id,
                                  Locations = new List<Location> { new Location(LocationBendName) },
                                  SportsPlayed = new List<SportWithSkillLevel>
                                                     {new SportWithSkillLevel
                                                            {
                                                                Name = Basketballname,
                                                                SkillLevel = new SkillLevel(3)
                                                            }}
                              };
            profileRepo.Save(profile);
            return profile;
        }

        public Profile CreateAccountWithProfile1()
        {
            CreateAccount1();
            return CreateProfileForAccount1();
        }

        public Profile CreateAccountWithProfile2()
        {
            CreateAccount2();
            return CreateProfileForAccount2();
        }

        public void CreateAccounts()
        {
            CreateAccount1();
            CreateAccount2();
            CreateAccount3();
        }

        public void CreateAccount1()
        {
            var userRepo = new UserRepository();
            userRepo.CreateUser("Simtay111@Gmail.com", "password");
        }

        public void CreateAccount2()
        {
            var userRepo = new UserRepository();
            userRepo.CreateUser("Cows@Moo.com", "password");
        }

        public void CreateAccount3()
        {
            var userRepo = new UserRepository();
            userRepo.CreateUser("Pickles@Moo.com", "password");
        }

        public void CreateSoccerSport()
        {
            var sportRepo = new SportRepository();
            sportRepo.CreateSport(SoccerName);
        }

        public void CreateBasketballSport()
        {
            var sportRepo = new SportRepository();
            sportRepo.CreateSport(Basketballname);
        }

        public void CreateFootballSport()
        {
            var sportRepo = new SportRepository();
            sportRepo.CreateSport(FootballName);
        }

        public void CreateLocationBend()
        {
            var locRepo = new LocationRepository();
            locRepo.CreateLocation(LocationBendName);
        }

        public void CreateLocationPortland()
        {
            var locRepo = new LocationRepository();
            locRepo.CreateLocation(LocationPortland);
        }

        public void CreateFriendshipForProfile1And2()
        {
            var profileRepo = new ProfileRepository();

            profileRepo.AddFriendToProfile(Profile1Id, Profile2Id);
        }

        public Team CreateTeam1()
        {
            var teamRepo = new TeamRepository();
            var team = new Team
                           {
                               MaxSize = 14,
                               Name = TeamName1,
                           };
            TeamId1 = team.Id;

            teamRepo.Save(team);
            return team;
        }

        public Team CreateTeam2()
        {
            var teamRepo = new TeamRepository();
            var team = new Team
            {
                MaxSize = 3,
                Name = TeamName2,
                PlayersIds = new List<string> { Profile1Id, Profile2Id },
                Creator = Profile1Id
            };

            TeamId2 = team.Id;

            teamRepo.Save(team);
            return team;
        }

        public Team CreateTeam3()
        {
            var teamRepo = new TeamRepository();
            var team = new Team
            {
                MaxSize = 3,
                Name = TeamName3,
                PlayersIds = new List<string>(),
                Creator = Profile2Id
            };

            TeamId3 = team.Id;

            teamRepo.Save(team);
            return team;
        }

        public PickUpGame CreateGameWithProfile1()
        {
            var game = new PickUpGame(DateTime.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = SoccerName;
            game.Location = LocationBendName;
            game.AddPlayer(Profile1Id);
            game.Creator = Profile1Id;
            game.GameName = "1 Player game";

            new PickUpGameRepository().SavePickUpGame(game);
            PickUpGame = game;
            return game;
        }

        public PickUpGame CreateGameWithProfile1AndProfile2()
        {
            var game = new PickUpGame(DateTime.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = Basketballname;
            game.Location = LocationPortland;
            game.AddPlayer(Profile1Id);
            game.AddPlayer(Profile2Id);
            game.Creator = Profile1Id;
            game.GameName = "Cool Game";

            new PickUpGameRepository().SavePickUpGame(game);
            PickUpGame = game;
            return game;
        }

        public TeamGame CreateGameWithTeams1And2()
        {
            var game = new TeamGame(DateTime.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = SoccerName;
            game.Location = LocationBendName;
            game.AddTeam(TeamId1);
            game.AddTeam(TeamId2);
            game.Creator = Profile2Id;

            new TeamGameRepository().SaveTeamGame(game);
            GameWithTeamsId = game.Id;
            return game;
        }


    }

    internal class GenericNodeType
    {
    }
}