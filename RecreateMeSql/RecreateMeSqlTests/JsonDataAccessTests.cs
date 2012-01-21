using NUnit.Framework;
using RecreateMeSql;

namespace RecreateMeSqlTests
{
    [TestFixture]
    public class JsonDataAccessTests
    {
         [Test]
        public void CanReadFromFiles()
         {
             var dataAccess = new JsonDataAccess();

             var testObject = new PersistentTestObject() {Id = "User1", Value = 2};

             var successfulWrite = dataAccess.WriteToJson(testObject, testObject.Id);

             Assert.True(successfulWrite);
         }

        [Test]
        public void CanWriteToFile()
        {
            var dataAccess = new JsonDataAccess();

            var testObj = dataAccess.GetByFileName<PersistentTestObject>("User1");

            Assert.That(testObj.Id, Is.EqualTo("User1"));
            Assert.That(testObj.Value, Is.EqualTo(2));
        }
    }

    public class PersistentTestObject
    {
        public string Id { get; set; }
        public int Value { get; set; }
    }
}