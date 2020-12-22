using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitBananas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace FitBananas.Controllers
{
    public class BananaController : Controller
    {
        
        private readonly BananaContext _context;

        // hardcoding stravaId temporarily
        private static int brianId = 56614892;
        private static int treyId = 24299518;

        private readonly int stravaId = brianId;

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
            return View();
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

        [HttpGet("strava/auth")]
        public IActionResult AuthorizeStrava()
        {
            // API call to retrieve athlete info
            Athlete currentAthlete = loadAthleteInfo().Result;
            // API call to retrieve athlete stats
            AthleteStats currentStats = loadAthleteStats(currentAthlete.Id).Result;
            Athlete dbAthlete = _context.Athletes.FirstOrDefault(athlete => athlete.Id == currentAthlete.Id);
            
            if(dbAthlete == null)
            {
                // Add a new athlete to the database
                _context.Add(currentAthlete);
                _context.SaveChanges();
                dbAthlete = currentAthlete;
                
                var newAthleteStats = new AthleteStats();
                newAthleteStats.AthleteId = dbAthlete.AthleteId;
                // Add a new AthleteStats to database
                _context.Add(newAthleteStats);
                _context.SaveChanges();

                int newAthleteStatsId = newAthleteStats.AthleteStatsId;
                // Pull year to date totals from the AthleteStats object created by API call
                BikeTotal newBikeTotal = currentStats.YTD_Ride_Totals;
                newBikeTotal.AthleteStatsId = newAthleteStatsId;
                _context.Add(newBikeTotal);
                RunTotal newRunTotal = currentStats.YTD_Run_Totals;
                newRunTotal.AthleteStatsId = newAthleteStatsId;
                _context.Add(newRunTotal);
                SwimTotal newSwimTotal = currentStats.YTD_Swim_Totals;
                newSwimTotal.AthleteStatsId = newAthleteStatsId;
                _context.Add(newSwimTotal);
                _context.SaveChanges();
            }
            else
            {
                // Update user's totals in the databaase
                // Get the Total that belongs to the UserStats of the User 
                BikeTotal bikeTotalToUpdate = _context.BikeTotals
                    .Include(bikeTotal => bikeTotal.UserStats)
                    .First(bikeTotal => bikeTotal.UserStats.AthleteId == dbAthlete.AthleteId);
                bikeTotalToUpdate.Distance = currentStats.YTD_Ride_Totals.Distance;
                bikeTotalToUpdate.Elevation_Gain = currentStats.YTD_Ride_Totals.Elevation_Gain;
                bikeTotalToUpdate.UpdatedAt = DateTime.Now;
                
                RunTotal runTotalToUpdate = _context.RunTotals
                    .Include(runTotal => runTotal.UserStats)
                    .First(runTotal => runTotal.UserStats.AthleteId == dbAthlete.AthleteId);
                runTotalToUpdate.Distance = currentStats.YTD_Run_Totals.Distance;
                runTotalToUpdate.Elevation_Gain = currentStats.YTD_Run_Totals.Elevation_Gain;
                runTotalToUpdate.UpdatedAt = DateTime.Now;
                
                SwimTotal swimTotalToUpdate = _context.SwimTotals
                    .Include(swimTotal => swimTotal.UserStats)
                    .First(swimTotal => swimTotal.UserStats.AthleteId == dbAthlete.AthleteId);
                swimTotalToUpdate.Distance = currentStats.YTD_Swim_Totals.Distance;
                swimTotalToUpdate.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
            }
            HttpContext.Session.SetInt32("UserId", dbAthlete.AthleteId);
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