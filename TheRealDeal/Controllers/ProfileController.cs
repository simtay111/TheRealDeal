using System;
using System.Web;
using System.Web.Mvc;
using RecreateMe;
using RecreateMe.Profiles;
using RecreateMe.Profiles.Handlers;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;
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

            var handler = new GetProfilesForAccountRequestHandle(new ProfileRepository());

            var response = handler.Handle(request);

            if (response.Profiles.Count == 0)
                return RedirectToAction("CreateProfile");

            ViewData[ViewDataConstants.Profiles] = response.Profiles;

            var cookie = HttpContext.Request.Cookies[Constants.CookieName] ??
            new HttpCookie(Constants.CookieName) { Expires = DateTime.Now.AddHours(1) };
            cookie.Values[Constants.CurrentProfileCookieField] = response.Profiles[0].ProfileId;

            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
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
            return View(CreateViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateProfile(CreateProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(CreateViewModel());
            }

            var request = new CreateProfileRequest(User.Identity.Name, model.Name, model.Location, model.Sports,
                                                   model.SkillLevel);

            var handler = new CreateProfileRequestHandle(new SportRepository(), new LocationRepository(),
                                                          new ProfileRepository(), new ProfileBuilder());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                return RedirectToAction("ChooseProfile");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(CreateViewModel());
        }

        private CreateProfileModel CreateViewModel()
        {
            var sports = new SportRepository().GetNamesOfAllSports();
            var locations = new LocationRepository().GetNamesOfAllLocations();

            var availableSports = new SelectList(sports);
            var availableLocations = new SelectList(locations);

            var viewModel = new CreateProfileModel
                                {
                                    AvailableSports = availableSports,
                                    AvailableLocations = availableLocations,
                                    AvailableSkillLevels = new SelectList(new SkillLevelProvider().GetListOfAvailableSkillLevels())
                                };
            return viewModel;
        }
    }
}
