using System.Web.Mvc;

namespace TheRealDeal.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
         public ActionResult Index()
        {

            var moo = HttpContext.Request.Cookies[Constants.CookieName].Values[Constants.CurrentProfileCookieField];

             return View();
         }
    }
}