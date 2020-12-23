using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FitBananas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace FitBananas.Controllers
{
    public class BananaController : Controller
    {
        
        private readonly BananaContext _context;
        private readonly int stravaId = AccessToken.StravaId;

        public BananaController(BananaContext context)
        {
            _context = context;
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            HttpContext.Session.SetInt32("UserId", 1); // ***************************************Dev setting
            HomeViewModel homeViewModel = new HomeViewModel(_context, stravaId);
            return View(homeViewModel);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            HttpContext.Session.SetInt32("UserId", 1); // ***************************************Dev setting
            NewChallengeViewModel viewModel = new NewChallengeViewModel(_context, stravaId);
            return View(viewModel);
        }

        [HttpGet("settings")]
        public IActionResult Settings()
        {
            HttpContext.Session.SetInt32("UserId", 1); // ***************************************Dev setting
            int? userId = HttpContext.Session.GetInt32("UserId");
            SettingsViewModel viewModel = new SettingsViewModel(_context, userId ?? 0);
            return View(viewModel);
        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("challenge/accept")]
        public IActionResult AcceptChallenge(int challengeId)
        {
            AthleteChallenge acceptedChallenge = new AthleteChallenge();
            acceptedChallenge.AthleteId = (int)HttpContext.Session.GetInt32("UserId");
            acceptedChallenge.ChallengeId = challengeId;
            _context.Add(acceptedChallenge);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost("units/select")]
        public IActionResult SelectUnits(string units)
        {
            Console.WriteLine($"Units changed to {units}");
            var currentAthlete = _context.Athletes.Find(HttpContext.Session.GetInt32("UserId"));
            if(units == "metric")
            {
                currentAthlete.MetricUnits = true;
            }
            else
            {
                currentAthlete.MetricUnits = false;
            }
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }

        [HttpPost("challenge/remove")]
        public IActionResult RemoveChallenge(int challengeId)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            var challengeToRemove = _context.AthleteChallenges
                .FirstOrDefault(challenge => challenge.AthleteId == userId && challenge.ChallengeId == challengeId);
            _context.Remove(challengeToRemove);
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }
    }
}