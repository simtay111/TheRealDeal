﻿using System;
using System.Linq;
using System.Web.Mvc;
using RecreateMe;
using RecreateMe.Scheduling.Games;
using RecreateMe.Scheduling.Handlers;
using RecreateMeSql.Repositories;
using TheRealDeal.Models.Games;

namespace TheRealDeal.Controllers
{
    public class GamesController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var request = new GetGamesForProfileRequest { ProfileId = GetProfileFromCookie() };
            var handler = new GetGamesForProfileRequestHandler(new PickUpGameRepository(),new TeamGameRepository() );
            var response = handler.Handle(request);

            var model = new ListOfGamesModel { TeamGames = response.TeamGames,
                PickUpGames = response.PickupGames,
                CurrentProfile = GetProfileFromCookie() };

            return View(model);
        }

        [Authorize]
        public ActionResult SearchForGame()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SearchForGame(SearchForGameModel model)
        {
            var request = new SearchForPickupGameRequest {Location = model.Location, Sport = model.Sport};

            var handler = new SearchForPickupGameRequestHandler(new PickUpGameRepository());

            var response = handler.Handle(request);

            //model.Results = response.GamesFound;

            return View(model);
        }

        [Authorize]
        public ActionResult CreateGame()
        {
            return View(CreateViewModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateGame(CreateGameModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(CreateViewModel());
            }

            var request = new CreatePickupGameRequest
                              {
                                  DateTime = DateTime.Now.ToString(),
                                  IsPrivate = model.IsPrivate,
                                  Location = model.Location,
                                  MaxPlayers = model.MaxPlayers,
                                  MinPlayers = model.MinPlayers,
                                  Sport = model.Sport,
                                  Creator = GetProfileFromCookie()
                              };

            var handler = new CreatePickupGameRequestHandler(new SportRepository(), new LocationRepository(),
                                                       new PickUpGameRepository(), new GameFactory());

            var response = handler.Handle(request);
            if (response.Status != ResponseCodes.Success)
                throw new NotImplementedException();

                var joinRequest = new JoinGameRequest { GameId = response.GameId, ProfileId = GetProfileFromCookie() };

                var joinHandler = new JoinGameRequestHandler(new PickUpGameRepository());

                var joinResponse = joinHandler.Handle(joinRequest);

                if (joinResponse.Status != ResponseCodes.Success)
                    throw new NotImplementedException();

                return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddTeamToGame(string gameId)
        {
            var teams = new TeamRepository().GetTeamsForProfile(GetProfileFromCookie());

            var model = new AddTeamToGameModel {TeamsForProfile = teams, GameId = gameId};

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddTeamToGame(AddTeamToGameModel model)
        {
            var request = new AddTeamToGameRequest() {GameId = model.GameId, TeamId = model.TeamId};

            var handler = new AddTeamToGameRequestHandler(new TeamGameRepository());

            var response = handler.Handle(request);

            if (response.Status != ResponseCodes.Success)
                throw new NotImplementedException();
            
            return RedirectToAction("Index");
        }



        [Authorize]
        public ActionResult JoinGame(string gameId)
        {
            var request = new JoinGameRequest {GameId = gameId, ProfileId = GetProfileFromCookie()};

            var handler = new JoinGameRequestHandler(new PickUpGameRepository());

            var response = handler.Handle(request);

            if (response.Status == ResponseCodes.Success)
                return RedirectToAction("Index");

            var errorMessage = response.Status.GetMessage();
            ModelState.AddModelError("", errorMessage);

            return RedirectToAction("SearchForGame");
        }

        private string GetProfileFromCookie()
        {
            var cookie = HttpContext.Request.Cookies[Constants.CookieName];
            if (cookie == null) RedirectToAction("ChooseProfile", "Profile");
            return cookie.Values[Constants.CurrentProfileCookieField];
        }

        private CreateGameModel CreateViewModel()
        {
            var model = new CreateGameModel { AvailableSports = new SportRepository().GetNamesOfAllSports() };
            var profile = new ProfileRepository().GetByProfileId(GetProfileFromCookie());
            model.AvailableLocations = profile.Locations.Select(x => x.Name).ToList();
            return model;
        }
    }
}