using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitBananas.Models;

namespace FitBananas.Controllers
{
    public class BananaController : Controller
    {
        
        private readonly BananaContext _context;

        // hardcoding userid temporarily
        private readonly int userId = 24299518;

        public BananaController(BananaContext context)
        {
            _context = context;
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            // string test = loadAthleteInfo().Result.FirstName;
            // ViewBag.Test = test;
            // // if (test == null)
            // // {
            // //     Console.WriteLine("null");
            // // }
            // // else {
            // //     Console.WriteLine(test);
            // // }
            return View();
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            NewChallengeViewModel viewModel = new NewChallengeViewModel(_context, userId);
            return View();
        }

        [HttpGet("settings")]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("strava/auth")]
        public IActionResult AuthorizeStrava()
        {
            Athlete currentAthlete = loadAthleteInfo().Result;
            Athlete dbAthlete = _context.Athletes.FirstOrDefault(athlete => athlete.Id == currentAthlete.Id);
            if(dbAthlete == null)
            {
                _context.Add(currentAthlete);
                _context.SaveChanges();
                dbAthlete = currentAthlete;
            }
            AthleteStats currentStats = loadAthleteStats(dbAthlete.Id).Result;

            return RedirectToAction("Home");
        }
        public static async Task<Athlete> loadAthleteInfo()
        {
            return await AthleteProcessor.LoadAthleteInformation();
            
        }

        public static async Task<AthleteStats> loadAthleteStats(int athleteId)
        {
            return await AthleteStatsProcessor.LoadAthleteStatsInfo(athleteId);
        }
    }
}