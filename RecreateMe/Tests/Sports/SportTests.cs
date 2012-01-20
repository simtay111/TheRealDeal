using NUnit.Framework;
using RecreateMe.Sports;

namespace RecreateMe.Tests.Sports
{
    [TestFixture]
    public class SportTests
    {
        private Sport _sport;

        [SetUp]
         public void SetUp()
         {
             _sport = new Sport();
         }

        [Test]
        public void HasAName()
        {
            const string name = "Volleyball";
            _sport.Name = name;
            Assert.That(_sport.Name, Is.EqualTo(name));
        }
    }
}