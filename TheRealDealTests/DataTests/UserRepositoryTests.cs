using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Neo4jRestNet.Core;
using Neo4jRestNet.GremlinPlugin;
using Neo4jRestNet.Rest;
using RecreateMe.Profiles;
using RecreateMeSql;

namespace TheRealDealTests.DataTests
{
    [TestFixture]
    [Category("Integration")]
    public class UserRepositoryTests
    {
        private UserRepository _userRepo;

        [SetUp]
        public void SetUp()
        {
            _userRepo = new UserRepository();
        }

        [Test]
        public void CanCreateNewUser()
        {
           // const string userName = "Billy@Bob.com";
           // const string password = "password1";
            
           // _userRepo.CreateUser(userName, password);

           // var rootNode = Node.GetRootNode();

           // var travRel = new TraverseRelationship("Account", RelationshipDirection.All);

           //var nodes = rootNode.Traverse(Order.BreadthFirst, Uniqueness.None, new List<TraverseRelationship>{ travRel},PruneEvaluator.None, ReturnFilter.AllButStartNode, 1, ReturnType.Node);

            

           // var nodesList = nodes as List<Node>;

           // var nodesy = nodesList.Where(x => (string) x.Properties.GetProperty("Username") == "Billy@Bob.com");


           // foreach (Node graphObject in nodes)
           // {
           //     Assert.NotNull(graphObject);
           // }

           // Neo4jRestApi.GetNode();

           // //var node = Node.GetNode();
            //Assert.NotNull(node);
        }

        [Test]
        public void CanSeeIfUserAlreadyExists()
        {
            const string userName = "Billy@Bob.com";

            var userAlreadyExists = _userRepo.AlreadyExists(userName);

            Assert.True(userAlreadyExists);
        }
    }
}