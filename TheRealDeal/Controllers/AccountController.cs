using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Security;
using FormsAuthenticationExtensions;
using RecreateMe;
using RecreateMe.Login.Handlers;
using RecreateMeSql;
using TheRealDeal.Models.Account;

namespace TheRealDeal.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogOnModel model)
        {
            var handler = new LoginRequestHandler(new UserRepository());

            var request = new LoginRequest() { Password = model.Password, Username = model.UserName };

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                var ticketData = new NameValueCollection
                {
                    { "Profile", "Start1" }
                };

                new FormsAuthentication().SetAuthCookie(HttpContext.User.Identity.Name, true, ticketData);

                var ticketDat2a = new NameValueCollection
            {
                { "Profile", "BorkBork" },
            };
                new FormsAuthentication().SetAuthCookie(User.Identity.Name, true, ticketDat2a);

                return RedirectToAction("ChooseProfile", "Profile");
            }

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var handler = new RegisterUserHandler(new UserRepository());

            var request = new RegisterUserRequest()
                              {
                                  ConfirmPassword = model.ConfirmPassword,
                                  LoginEmail = model.UserName,
                                  Password = model.Password
                              };

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToAction("Index", "Home");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }
    }
}