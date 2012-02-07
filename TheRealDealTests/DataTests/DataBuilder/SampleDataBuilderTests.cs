using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Neo4jClient;
using Neo4jRestNet.Core;

namespace TheRealDealTests.DataTests.DataBuilder
{
    [TestFixture]
    public class SampleDataBuilderTests
    {
         [Test]
         public void CanEraseData()
         {
             var graphClient = new GraphClient(new Uri("http://localhost:7474/db/data"));

             graphClient.Connect();

             //var root = graphClient.Get(new RootNode());

             //var data = new MyType() { Moo = "Cow" };

             //var created = graphClient.Create(data);

             //graphClient.CreateRelationship(created, new AccountRelationship(root.Reference));

             var results = graphClient.ExecuteGetAllNodesGremlin<MyType>(
                 "g.v(0).in('Account')", new Dictionary<string, object>()).ToList();

             foreach(var result in results)
             {
                 var node = result.Reference;
                 var node1 = result.Data;
             }

             //graphClient.Delete(results[1].Reference, DeleteMode.NodeAndRelationships);

             //foreach (var result in results)
             //{
             //    graphClient.Delete(result.Reference, DeleteMode.NodeAndRelationships);
             //}

         }

        public class MyType

    {
        public string Moo { get; set; }
    }
    }
}