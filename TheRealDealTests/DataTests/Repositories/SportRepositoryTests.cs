using System;
using System.Linq;
using NUnit.Framework;
using Neo4jClient;
using Neo4jClient.Gremlin;
using RecreateMeSql;
using RecreateMeSql.Connection;
using RecreateMeSql.Relationships;
using RecreateMeSql.Repositories;
using RecreateMeSql.SchemaNodes;
using TheRealDealTests.DataTests.DataBuilder;

namespace TheRealDealTests.DataTests.Repositories
{
    [TestFixture]
    [Category("Integration")]
    public class SportRepositoryTests
    {
        private SportRepository _repo;
        private SampleDataBuilder _data = new SampleDataBuilder();

        [SetUp]
        public void SetUp()
        {
            _data.DeleteAllData();
            _repo = new SportRepository();
        }

        [Test]
        public void CanFindSportsByName()
        {
            _data.CreateSoccerSport();
            const string soccerName = "Soccer";

            var sport = _repo.FindByName(soccerName);

            Assert.NotNull(sport);
            Assert.That(sport.Name, Is.EqualTo(soccerName));
        }

        [Test]
        public void CanCreateSports()
        {
            _repo.CreateSport("Soccer");

            var sport = _repo.FindByName("Soccer");
            Assert.That(sport, Is.Not.Null);
        }

        [Test]
        public void CreatesSportsNodeIfItDoesNotExistWhenCreatingSports()
        {
            _repo.CreateSport("Soccer");

            var gc = GraphClientFactory.Create();
            var nodes = gc.RootNode.OutE(RelationsTypes.BaseNode.ToString())
                .InV<SchemaNode>(n => n.Type == SchemaNodeTypes.SportBase.ToString());
            Assert.True(nodes.Any());
        }

        [Test]
        public void CanGetListOfAllSports()
        {
            _data.CreateBasketballSport();
            _data.CreateFootballSport();
            _data.CreateSoccerSport();

            var sports = _repo.GetNamesOfAllSports();

            Assert.That(sports[0], Is.EqualTo("Basketball"));
            Assert.That(sports[1], Is.EqualTo("Football"));
            Assert.That(sports[2], Is.EqualTo("Soccer"));
        }
    }
}