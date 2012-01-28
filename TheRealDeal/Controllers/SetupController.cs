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
            var profileId = GetProfileCookie();

            var sportsForProfile = GetSportsForProfile(profileId);

            var sportRepo = new SportRepository();

            var sports = sportRepo.GetNamesOfAllSports();

            var leftOverSportsToPotentiallyAdd =
                sports.Where(x => sportsForProfile.FirstOrDefault(y => y.Name == x) == null).ToList();



            ViewData[ViewDataConstants.AvailableSports] = new SelectList(leftOverSportsToPotentiallyAdd);
            ViewData[ViewDataConstants.SportsForProfile] = sportsForProfile;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Sports(AddSportModel model)
        {
            var profile = GetProfileCookie();
            var request = new AddSportToProfileRequest() {SkillLevel = 1, Sport = model.ChosenSport, UniqueId = profile};

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

        private string GetProfileCookie()
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
