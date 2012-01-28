using System;
using System.Web;
using System.Web.Mvc;
using RecreateMe;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;
using RecreateMeSql;
using TheRealDeal.Models.Profile;

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

            ViewData[ViewDataConstants.Profiles] = response.Profiles;

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

        [Authorize]
        public ActionResult CreateProfile()
        {
            var sportRepo = new SportRepository();

            var sports = sportRepo.GetNamesOfAllSports();
            
            var selectList = new SelectList(sports);

            ViewBag.ListOfSports = selectList;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateProfile(CreateProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var request = new CreateProfileRequest(User.Identity.Name, model.Name, model.Location, model.Sports,
                                                   model.SkillLevel);

            var handler = new CreateProfileRequestHandler(new SportRepository(), new LocationRepository(),
                                                          new ProfileRepository(), new ProfileBuilder());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                return RedirectToAction("ChooseProfile");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }
    }
}
