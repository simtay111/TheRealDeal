using System.Web.Mvc;
using RecreateMe.Teams;
using RecreateMe.Teams.Handlers;
using RecreateMeSql;
using TheRealDeal.Models.Teams;

namespace TheRealDeal.Controllers
{
    public class TeamsController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var model = GetTeamsForCurrentProfileAndBuildModel();

            return View(model);
        }

        [Authorize]
        public ActionResult CreateTeam()
        {
            return View();
        }

        private TeamsViewModel GetTeamsForCurrentProfileAndBuildModel()
        {
            var profileId = GetProfileFromCookie();

            var request = new GetTeamsForProfileRequest() {ProfileId = profileId};

            var response = new GetTeamsForProfileHandler(new TeamRepository()).Handle(request);

            var model = new TeamsViewModel() {Teams = response.Teams};
            return model;
        }

        private string GetProfileFromCookie()
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName];
            if (cookie == null) RedirectToAction("ChooseProfile", "Profile");
            return cookie.Values[Constants.CurrentProfileCookieField];
        }
    }
}