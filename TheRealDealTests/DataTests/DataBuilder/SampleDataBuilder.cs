using System;
using System.Collections.Generic;
using System.Linq;
using Neo4jClient;
using RecreateMe.Locales;
using RecreateMe.Profiles;
using RecreateMe.Sports;
using RecreateMeSql;
using RecreateMeSql.Repositories;

namespace TheRealDealTests.DataTests.DataBuilder
{
    public class SampleDataBuilder
    {
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

            var profile = new Profile()
                              {
                                  AccountId = "Cows@Moo.com",
                                  ProfileId = "Profile1",
                                  Locations = new List<Location> { new Location("Bend") },
                                  SportsPlayed = new List<SportWithSkillLevel>
                                                     {new SportWithSkillLevel
                                                          {
                                                            Name = "Soccer",
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
                                  ProfileId = "Simtay111",
                                  Locations = new List<Location>() { new Location("Bend") },
                                  SportsPlayed = new List<SportWithSkillLevel>() {new SportWithSkillLevel
                                                                                      {
                                                                                          Name = "Basketball",
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
            sportRepo.CreateSport("Soccer");
        }

        public void CreateBasketballSport()
        {
            var sportRepo = new SportRepository();
            sportRepo.CreateSport("Basketball");
        }

        public void CreateFootballSport()
        {
            var sportRepo = new SportRepository();
            sportRepo.CreateSport("Football");
        }

        public void CreateLocationBend()
        {
            var locRepo = new LocationRepository();
            locRepo.CreateLocation("Bend");
        }

        public void CreateLocationPortland()
        {
            var locRepo = new LocationRepository();
            locRepo.CreateLocation("Portland");
        }

        public void CreateFriendshipForProfile1And2()
        {
            var profileRepo = new ProfileRepository();

            profileRepo.AddFriendToProfile("Simtay111", "Profile1");
        }
    }

    internal class GenericNodeType
    {
    }
}