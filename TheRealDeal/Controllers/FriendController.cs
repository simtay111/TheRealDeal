using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecreateMe.Friends.Handlers;
using RecreateMe.Friends.Search;
using RecreateMeSql;
using TheRealDeal.Models.Friend;

namespace TheRealDeal.Controllers
{
    public class FriendController : Controller
    {
        //
        // GET: /Friend/

        [Authorize]
        public ActionResult FriendList()
        {
            var friends = new ProfileRepository().GetFriendIdAndNameListForProfile(GetProfileFromCookie());

            ViewData[ViewDataConstants.FriendsDictionary] = friends;

            return View();
        }

        [Authorize]
        public ActionResult Search()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Search(SearchForFriendModel model)
        {
            var request = new SearchForFriendsRequest
                              {
                Location = model.Location,
                ProfileName = model.ProfileName,
                Sport = model.Sport
            };

            var handler = new SearchForFriendsRequestHandler(new ProfileRepository());

            var response = handler.Handle(request);

            model.Results = response.Results;

            return View(model);
        }

        public ActionResult Add(string friendId)
        {
            var request = new AddPlayerToFriendsRequest()
                              {
                                  FriendId = friendId,
                                  ProfileId = GetProfileFromCookie()
                              };

            var handler = new AddPlayerToFriendsRequestHandler(new ProfileRepository());

            handler.Handle(request);

            return RedirectToAction("FriendList");
        }

        private string GetProfileFromCookie()
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName];
            if (cookie == null) RedirectToAction("ChooseProfile", "Profile");
            return cookie.Values[Constants.CurrentProfileCookieField];
        }
    }
}
