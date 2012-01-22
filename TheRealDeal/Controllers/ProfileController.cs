using System.Web.Mvc;
using Moq;
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
        public ActionResult ChooseProfile(string accountName)
        {
            var request = new GetProfilesForAccountRequest
                              {
                                  Account = accountName
                              };

            var handler = new GetProfilesForAccountRequestHandler(new ProfileRepository());

            var response = handler.Handle(request);

            ViewData["Profiles"] = response.Profiles;

            return View();
        }


        

    }
}
