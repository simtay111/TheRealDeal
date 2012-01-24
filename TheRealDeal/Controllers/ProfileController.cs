using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Moq;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;
using RecreateMeSql;
using TheRealDeal.Models.Profile;
using FormsAuthenticationExtensions;

namespace TheRealDeal.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /ChooseProfile/
        [Authorize]
        public ActionResult ChooseProfile()
        {
            var request = new GetProfilesForAccountRequest
                              {
                                  Account = HttpContext.User.Identity.Name
                              };

            var handler = new GetProfilesForAccountRequestHandler(new ProfileRepository());

            var response = handler.Handle(request);

            ViewData["Profile"] = response.Profiles;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChooseProfile(ChooseProfileModel model)
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName] ??
                new HttpCookie(Constants.CookieName) { Expires = DateTime.Now.AddHours(1) };
            cookie.Values[Constants.CurrentProfileCookieField] = model.Profile;

            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }
    }
}
