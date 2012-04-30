using System.Web.Mvc;
using RecreateMe.Friends.Invites.Handlers;
using RecreateMeSql.Repositories;

namespace TheRealDeal.Controllers
{
    public class InvitesController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var request = new GetCurrentGameInviteHandle(new InviteRepository(), new PickUpGameRepository());

            return View();
        }
    }
}