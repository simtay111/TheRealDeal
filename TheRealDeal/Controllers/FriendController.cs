using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecreateMeSql;

namespace TheRealDeal.Controllers
{
    public class FriendController : Controller
    {
        //
        // GET: /Friend/

        public ActionResult Index()
        {
            var friends = new ProfileRepository().GetFriendIdAndNameListForProfile(GetProfileFromCookie());

            ViewData[ViewDataConstants.FriendsDictionary] = friends;

            return View();
        }

        private string GetProfileFromCookie()
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName];
            if (cookie == null) RedirectToAction("ChooseProfile", "Profile");
            return cookie.Values[Constants.CurrentProfileCookieField];
        }


    }
}
