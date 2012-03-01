using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Scheduling.Handlers.Games;
using RecreateMe.Sports;
using RecreateMe.Teams;
using RecreateMeSql;
using RecreateMeSql.Repositories;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class SampleDataBuilder
    {
        private string _teamId2;
        private string _teamId1;
        public GameWithoutTeams GameWithoutTeams;
        private const string LocationPortland = "Portland";
        private const string Profile2Id = "Profile1";
        private const string SoccerName = "Soccer";
        private const string LocationBendName = "Bend";
        private const string TeamName1 = "Team1";
        private const string TeamName2 = "Team2";
        private const string Profile1Id = "Simtay111";
        private const string FootballName = "Football";
        private const string Basketballname = "Basketball";

        public void DeleteAllData()
        {
            var graphClient = BuildGraphClient();

            var nodes = graphClient.ExecuteGetAllNodesGremlin<GenericNodeType>
                ("g.V.filter{it.Id != 0}", new Dictionary<string, object>()).ToList();

            nodes.RemoveAt(0);

            foreach (var node in nodes)
            {
                graphClient.Delete(node.Reference, DeleteMode.NodeAndRelationships);
            }
        }

        private GraphClient BuildGraphClient()
        {
            var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

            graphClient.Connect();
            return graphClient;
        }

        public void CreateData()
        {
            CreateAccounts();
            CreateLocations();
            CreateSports();
            CreateProfilesForAccounts();
            CreateFriendship();
            CreateTeams();
            CreateGames();
        }

        private void CreateGames()
        {
            CreateGameWithProfile1();
            CreateGameWithTeams1And2();
        }

        private void CreateTeams()
        {
            CreateTeam1();
            CreateTeam2();
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
                                  AccountId = "Cows@Moo.com",
                                  ProfileId = Profile2Id,
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
                                  AccountId = "Simtay111@Gmail.com",
                                  ProfileId = Profile1Id,
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
                               Name = TeamName1
                           };
            _teamId1 = team.Id;

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
                PlayersIds = new List<string>() { Profile1Id, Profile2Id}
            };

            _teamId2 = team.Id;

            teamRepo.Save(team);
            return team;
        }

        public GameWithoutTeams CreateGameWithProfile1()
        {
            var game = new GameWithoutTeams(DateTimeOffset.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = new Sport(SoccerName);
            game.Location = new Location(LocationBendName);
            game.AddPlayer(Profile1Id);

            new GameRepository().Save(game);
            GameWithoutTeams = game;
            return game;
        }

        public GameWithTeams CreateGameWithTeams1And2()
        {
            var game = new GameWithTeams(DateTimeOffset.Now, new Sport(), new Location());
            game.MaxPlayers = 5;
            game.MinPlayers = 3;
            game.IsPrivate = true;
            game.Sport = new Sport(SoccerName);
            game.Location = new Location(LocationBendName);
            game.AddTeam(_teamId1);
            game.AddTeam(_teamId2);

            new GameRepository().Save(game);
            return game;
        }


    }

    internal class GenericNodeType
    {
    }
}