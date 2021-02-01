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

        public BananaController(BananaContext context)
        {
            _context = context;
        }

        [HttpGet("home")]
        public IActionResult Home()
        {
            int athleteId = HttpContext.Session.GetInt32("AthleteId") ?? 0;
            UpdateAthlete(athleteId);
            HomeViewModel homeViewModel = new HomeViewModel(_context, athleteId);
            return View(homeViewModel);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            int? athleteId = HttpContext.Session.GetInt32("AthleteId");
            NewChallengeViewModel viewModel = new NewChallengeViewModel(_context, athleteId ?? 0);
            return View(viewModel);
        }

        [HttpGet("settings")]
        public IActionResult Settings()
        {
            int? athleteId = HttpContext.Session.GetInt32("AthleteId");
            SettingsViewModel viewModel = new SettingsViewModel(_context, athleteId ?? 0);
            return View(viewModel);
        }

        [HttpGet("settings/purge")]
        public IActionResult Purge()
        {
            int athleteId = HttpContext.Session.GetInt32("AthleteId") ?? 0;
            Token token = _context.Athletes
                .Include(athlete => athlete.Token)
                .FirstOrDefault(athlete => athlete.AthleteId == athleteId)
                .Token;
            // If token expires with one hour, refresh token
            if (token.ExpiresAt < DateTime.Now.AddSeconds(3600)) {
                AuthorizationModel tokenModel = StravaController
                    .loadNewToken(token.RefreshToken).Result;
                token.AccessToken = tokenModel.Access_Token;
                token.RefreshToken = tokenModel.Refresh_Token;
                token.ExpiresAt = DateTime.Now.AddSeconds(tokenModel.Expires_In);
                token.ExpiresIn = TimeSpan.FromSeconds(tokenModel.Expires_In);
            }
            Processor.Deauthorization(token.AccessToken);
            Athlete athleteToDelete = _context.Athletes
                .FirstOrDefault(athlete => athlete.AthleteId == athleteId);
            _context.Remove(athleteToDelete);
            _context.Remove(token);
            _context.SaveChanges();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("challenge/accept")]
        public IActionResult AcceptChallenge(int challengeId)
        {
            AthleteChallenge acceptedChallenge = new AthleteChallenge();
            acceptedChallenge.AthleteId = (int)HttpContext.Session.GetInt32("AthleteId");
            acceptedChallenge.ChallengeId = challengeId;
            _context.Add(acceptedChallenge);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpPost("units/select")]
        public IActionResult SelectUnits(string units)
        {
            Console.WriteLine($"Units changed to {units}");
            var currentAthlete = _context.Athletes.Find(HttpContext.Session.GetInt32("AthleteId"));
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
            int athleteId = (int)HttpContext.Session.GetInt32("AthleteId");
            var challengeToRemove = _context.AthleteChallenges
                .FirstOrDefault(challenge => challenge.AthleteId == athleteId && challenge.ChallengeId == challengeId);
            _context.Remove(challengeToRemove);
            _context.SaveChanges();
            return RedirectToAction("Settings");
        }

        public void UpdateAthlete(int athleteId)
        {
            // Retrieve token info for this athlete
            Token token = _context.Athletes
                .Include(athlete => athlete.Token)
                .FirstOrDefault(athlete => athlete.AthleteId == athleteId)
                .Token;
            // If token expires with one hour, refresh token
            if (token.ExpiresAt < DateTime.Now.AddSeconds(3600)) {
                AuthorizationModel tokenModel = StravaController
                    .loadNewToken(token.RefreshToken).Result;
                token.AccessToken = tokenModel.Access_Token;
                token.RefreshToken = tokenModel.Refresh_Token;
                token.ExpiresAt = DateTime.Now.AddSeconds(tokenModel.Expires_In);
                token.ExpiresIn = TimeSpan.FromSeconds(tokenModel.Expires_In);
            }
            // Retrieve current athlete from the database
            Athlete dbAthlete = _context.Athletes
                .FirstOrDefault(athlete => athlete.AthleteId == athleteId);
            // API call to retrieve athlete stats
            AthleteStats currentStats = StravaController
                .loadAthleteStats(dbAthlete.Id, token.AccessToken).Result;
            
            // Update user's totals in the database 
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
    }
}