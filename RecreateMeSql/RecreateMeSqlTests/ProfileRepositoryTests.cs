using System;
using System.Data;
using System.Data.SqlClient;
using Moq;
using NUnit.Framework;
using RecreateMe.Login;
using RecreateMe.Profiles;
using RecreateMeSql;

namespace RecreateMeSqlTests
{
    [TestFixture]
    public class ProfileRepositoryTests
    {
         [Test]
        public void WritesToJson()
         {
             var profile = new Profile {UniqueId = "123"};


             var repo = new ProfileRepository();

             var success  = repo.SaveOrUpdate(profile);

             Assert.True(success);
         }
    }
}