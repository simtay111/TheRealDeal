﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RecreateMe;
using RecreateMe.Configuration;
using RecreateMe.Locales;
using RecreateMe.Profiles.Handlers;
using RecreateMe.ProfileSetup.Handlers;
using RecreateMe.Sports;
using RecreateMeSql.Repositories;
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
            var handler = new GetListOfConfigurableProfileOptionsHandle(new ConfigurationProvider());

            var response = handler.Handle(new GetListOfConfigurableProfileOptionsRequest());

            ViewData["ListOfOptions"] = response.ListOfConfigurableOptions;

            var model = CreateSetupOptionsModel();

            return View(model);
        }

        private SetupOptionsModel CreateSetupOptionsModel()
        {
            var model = new SetupOptionsModel();

            var profileId = GetProfileFromCookie();

            model.SportsForProfile = GetSportsForProfile(profileId);
            model.CurrentLocations = GetLocationsForProfile(profileId);
            model.AvailableSports = new SelectList(new SportRepository().GetNamesOfAllSports());
            model.AvailableLocations = new SelectList(new LocationRepository().GetNamesOfAllLocations());
            model.SkillLevels = new SelectList(new SkillLevelProvider().GetListOfAvailableSkillLevels());
            return model;
        }

        [Authorize]
        public ActionResult Sports()
        {
            var model = CreateSportsModel();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Sports(SetupOptionsModel model)
        {
            if (string.IsNullOrEmpty(model.ChosenSport) || string.IsNullOrEmpty(model.ChosenSkillLevel))
            {
                ModelState.AddModelError("AvailableSports", "Please choose a sport AND a skill level");
                return View("SetupOptions", CreateSetupOptionsModel());
            }

            var profile = GetProfileFromCookie();
            var request = new AddSportToProfileRequest
                              {
                                  SkillLevel = int.Parse(model.ChosenSkillLevel),
                                  Sport = model.ChosenSport,
                                  UniqueId = profile
                              };

            var handler = new AddSportToProfileRequestHandle(new ProfileRepository(), new SportRepository());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                return View("SetupOptions", CreateSetupOptionsModel());
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return View("SetupOptions", CreateSetupOptionsModel());
        }

        //[Authorize]
        //public ActionResult Location()
        //{
        //    var model = GetLocationsForProfile();

        //    return View(model);
        //}

        [Authorize]
        [HttpPost]
        public ActionResult Location(SetupOptionsModel model)
        {
            if (string.IsNullOrEmpty(model.LocationToAdd))
            {
                ModelState.AddModelError("AvailableLocations", "Please choose a location");
                return View("SetupOptions", CreateSetupOptionsModel());
            }

            var profileId = GetProfileFromCookie();
            var request = new AddLocationToProfileRequest
                              {
                                  Location = model.LocationToAdd,
                                  ProfileId = profileId
                              };

            var handler = new AddLocationToProfileRequestHandle(new ProfileRepository(), new LocationRepository());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
            {
                return RedirectToAction("SetupOptions", "Setup");
            }

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return RedirectToAction("SetupOptions", "Setup");
        }

        [Authorize]
        public ActionResult RemoveSport(string sportName)
        {
            var request = new RemoveSportFromProfileRequest {ProfileId = GetProfileFromCookie(), SportName = sportName};

            var handler = new RemoveSportFromProfileRequestHandler(new ProfileRepository());

            handler.Handle(request);

            return RedirectToAction("SetupOptions", CreateSetupOptionsModel());
        }

        [Authorize]
        public ActionResult RemoveLocation(string locationName)
        {
            var request = new RemoveLocationFromProfileRequest { ProfileId = GetProfileFromCookie(), LocationName = locationName };

            var handler = new RemoveLocationFromProfileRequestHandler(new ProfileRepository());

            handler.Handle(request);

            return RedirectToAction("SetupOptions", CreateSetupOptionsModel());
        }

        private AddSportModel CreateSportsModel()
        {
            var profileId = GetProfileFromCookie();

            var sportsForProfile = GetSportsForProfile(profileId);

            var sportRepo = new SportRepository();

            var sports = sportRepo.GetNamesOfAllSports();

            var leftOverSportsToPotentiallyAdd =
                sports.Where(x => sportsForProfile.FirstOrDefault(y => y.Name == x) == null).ToList();


            var model = new AddSportModel
                            {
                                AvailableSports = new SelectList(leftOverSportsToPotentiallyAdd),
                                SportsForProfile = sportsForProfile,
                                SkillLevels = new SelectList(new SkillLevelProvider().GetListOfAvailableSkillLevels())
                            };
            return model;
        }

        private string GetProfileFromCookie()
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName];
            if (cookie == null) RedirectToAction("ChooseProfile", "Profile");
            return cookie.Values[Constants.CurrentProfileCookieField];
        }

        private IList<SportWithSkillLevel> GetSportsForProfile(string profileId)
        {
            var request = new GetSportsForProfileRequest { ProfileId = profileId };

            var handler = new GetSportsForProfileHandle(new ProfileRepository());

            var response = handler.Handle(request);
            return response.SportsForProfile;
        }

        private IList<Location> GetLocationsForProfile(string profileId)
        {
            var repo = new ProfileRepository();

            var profile = repo.GetByProfileId(profileId);

            return profile.Locations;
        }
    }
}
