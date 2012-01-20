using System.Web.Mvc;

namespace TheRealDeal.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
         public ActionResult Index()
         {
             return View();
         }
    }
}