using Moq;
using NUnit.Framework;
using RecreateMe.Profiles;
using RecreateMeSql;

namespace TheRealDealTests.DataTests
{
    [TestFixture]
    [Ignore]
    public class ProfileRepositoryTests
    {
        // [Test]
        //public void WritesToJson()
        // {
        //     var profile = new Profile {UniqueId = "123"};
        //     var mockJsonAccess = new Mock<IJsonDataAccess>();
        //     mockJsonAccess.Setup(x => x.WriteToJson(profile, profile.UniqueId)).Returns(true);
        //     var repo = new ProfileRepository(mockJsonAccess.Object);

        //     var success  = repo.SaveOrUpdate(profile);

        //     Assert.True(success);
        // }

        // [Test]
        // public void CanGetByUniqueId()
        // {
        //     var profileToReturn = new Profile { UniqueId = "123" };
        //     var mockJsonAccess = new Mock<IJsonDataAccess>();
        //     mockJsonAccess.Setup(x => x.GetByFileName<Profile>(profileToReturn.UniqueId)).Returns(profileToReturn);
        //     var repo = new ProfileRepository(mockJsonAccess.Object);

        //     var profile = repo.GetByUniqueId(profileToReturn.UniqueId);

        //     Assert.NotNull(profile);
        //     Assert.That((object) profile, Is.TypeOf<Profile>());
        // }
    }
}