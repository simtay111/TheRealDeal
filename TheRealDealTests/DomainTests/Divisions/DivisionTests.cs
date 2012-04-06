using NUnit.Framework;
using RecreateMe.Divisinos;

namespace TheRealDealTests.DomainTests.Divisions
{
    [TestFixture]
    public class DivisionTests
    {
        [Test]
        public void HasName()
        {
            var division = new Division();
            division.Name = "moo";
        }  
        
        [Test]
        public void HasId()
        {
            var division = new Division();
            Assert.That(division.Id, Is.Not.Null.Or.Empty);
        }
         
    }
}