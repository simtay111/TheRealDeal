﻿using System.Web.Mvc;
using System.Web.Security;
using RecreateMe;
using RecreateMe.Login.Handlers;
using RecreateMeSql.Repositories;
using TheRealDeal.Models.Account;

namespace TheRealDeal.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogIn()
        {
            RecreateMeSql.SqlServerDataHelper.BuildSchema();
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogOnModel model)
        {
            var handler = new LoginRequestHandle(new UserRepository());

            var request = new LoginRequest { Password = model.Password, Username = model.UserName };

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                return RedirectToAction("ChooseProfile", "Profile");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            var handler = new RegisterUserHandle(new UserRepository());

            var request = new RegisterUserRequest()
                              {
                                  ConfirmPassword = model.ConfirmPassword,
                                  LoginEmail = model.UserName,
                                  Password = model.Password
                              };

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToAction("ChooseProfile", "Profile");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View(model);
        }
    }
}