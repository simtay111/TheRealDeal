﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheRealDeal.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Setup/
        [Authorize]
        public ActionResult Index()
        {


            return View();
        }

    }
}
