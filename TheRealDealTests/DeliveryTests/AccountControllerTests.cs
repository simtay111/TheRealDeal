using NUnit.Framework;
using TheRealDeal.Controllers;
using TheRealDeal.Models.Account;

namespace TheRealDealTests.DeliveryTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        [Ignore]
        public void CanLogin()
        {
            var controller = new AccountController();
            var model = new LogOnModel()
                            {
                                Password = "123",
                                UserName = "UserName"
                            };

            var actionResult = controller.LogIn(model);

            Assert.That(actionResult, Is.Not.Null);
        }
    }
}
