using System;
using System.Web.Mvc;
using RecreateMe;
using RecreateMe.Teams.Handlers;
using RecreateMeSql.Repositories;
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

        [Authorize]
        [HttpPost]
        public ActionResult CreateTeam(CreateTeamModel model)
        {
            var request = new CreateTeamRequest
                              {
                                  MaxSize = model.MaxSize,
                                  Name = model.Name,
                                  ProfileId = GetProfileFromCookie()
                              };

            var handler = new CreateTeamRequestHandle(new TeamRepository());

            var response = handler.Handle(request);

            if (response.Status != ResponseCodes.Success)
                throw new NotImplementedException();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ViewTeam(string teamId)
        {
            var request = new ViewTeamRequest {TeamId = teamId};

            var handler = new ViewTeamRequestHandle(new TeamRepository());

            var response = handler.Handle(request);

            return View(new ViewTeamModel { Team = response.Team});
        }



        private TeamsViewModel GetTeamsForCurrentProfileAndBuildModel()
        {
            var profileId = GetProfileFromCookie();

            var request = new GetTeamsForProfileRequest {ProfileId = profileId};

            var response = new GetTeamsForProfileHandle(new TeamRepository()).Handle(request);

            var model = new TeamsViewModel {Teams = response.Teams};
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