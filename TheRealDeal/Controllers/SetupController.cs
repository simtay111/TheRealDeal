using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecreateMe.Configuration;
using RecreateMe.ProfileSetup.Handlers;

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

    }
}
