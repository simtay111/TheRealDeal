using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RecreateMe;
using RecreateMe.Configuration;
using RecreateMe.ProfileSetup.Handlers;
using RecreateMe.Profiles.Handlers;
using RecreateMe.Sports;
using RecreateMeSql;
using RecreateMeSql.Repositories;
using TheRealDeal.Models.Setup;

namespace TheRealDeal.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Setup/
        [Authorize]
        public ActionResult SetupOptions()
        {
            var handler = new GetListOfConfigurableProfileOptionsHandler(new ConfigurationProvider());

            var response = handler.Handle(new GetListOfConfigurableProfileOptionsRequest());

            ViewData["ListOfOptions"] = response.ListOfConfigurableOptions;

            return View();
        }

        [Authorize]
        public ActionResult Sports()
        {
            var profileId = GetProfileFromCookie();

            var sportsForProfile = GetSportsForProfile(profileId);

            var sportRepo = new SportRepository();

            var sports = sportRepo.GetNamesOfAllSports();

            var leftOverSportsToPotentiallyAdd =
                sports.Where(x => sportsForProfile.FirstOrDefault(y => y.Name == x) == null).ToList();


            ViewData[ViewDataConstants.SkillLevels] = new SelectList(new SkillLevelProvider()
                .GetListOfAvailableSkillLevels());
            ViewData[ViewDataConstants.AvailableSports] = new SelectList(leftOverSportsToPotentiallyAdd);
            ViewData[ViewDataConstants.SportsForProfile] = sportsForProfile;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Sports(AddSportModel model)
        {
            var profile = GetProfileFromCookie();
            var request = new AddSportToProfileRequest()
                              {
                                  SkillLevel = int.Parse(model.ChosenSkillLevel),
                                  Sport = model.ChosenSport, 
                                  UniqueId = profile
                              };

            var handler = new AddSportToProfileRequestHandler(new ProfileRepository(), new SportRepository());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                return RedirectToAction("Sports", "Setup");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }

        [Authorize]
        public ActionResult Location()
        {
            var repo = new ProfileRepository();

            var profile = repo.GetByProfileId(GetProfileFromCookie());

            ViewData[ViewDataConstants.CurrentLocations] = profile.Locations;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Location(AddLocationModel model)
        {
            var request = new AddLocationToProfileRequest()
                              {
                                  Location = model.LocationToAdd,
                                  ProfileId = GetProfileFromCookie()
                              };

            var handler = new AddLocationToProfileRequestHandler(new ProfileRepository(), new LocationRepository());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                return RedirectToAction("Location", "Setup");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }

        private string GetProfileFromCookie()
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName];
            if (cookie == null) RedirectToAction("ChooseProfile", "Profile");
            return cookie.Values[Constants.CurrentProfileCookieField];
        }

        private IList<SportWithSkillLevel> GetSportsForProfile(string profileId)
        {
            var request = new GetSportsForProfileRequest
                              {ProfileId = profileId};

            var handler = new GetSportsForProfileHandler(new ProfileRepository());

            var response = handler.Handle(request);
            return response.SportsForProfile;
        }
    }
}
