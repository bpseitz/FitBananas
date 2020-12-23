using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FitBananas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace FitBananas.Controllers
{
    public class StravaController : Controller
    {
        private readonly BananaContext _context;
        public StravaController(BananaContext context)
        {
            _context = context;
        }

        [HttpGet("exchange_token/")]
        public IActionResult ExchangeToken(string state, string code, string scope)
        {
            Console.WriteLine(state);
            Console.WriteLine(code);
            Console.WriteLine(scope);
            // send post request to strava api with code
            // to receive the access token and refresh token
            AuthorizationModel authModel =  loadAuthorization(code).Result;
            var newToken = new Token();
            newToken.ExpiresAt = DateTime.Now.AddSeconds(authModel.Expires_In);
            newToken.ExpiresIn = TimeSpan.FromSeconds(authModel.Expires_In);
            newToken.RefreshToken = authModel.Refresh_Token;
            newToken.AccessToken = authModel.Access_Token;
            Athlete currentAthlete = authModel.Athlete;
            int userId = UpdateAthlete(currentAthlete);
            HttpContext.Session.SetInt32("UserId", userId);

            return RedirectToAction("Home", "Banana");
        }

        public int UpdateAthlete(Athlete currentAthlete)
        {
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
            return dbAthlete.AthleteId;
        }
        public static async Task<Athlete> loadAthleteInfo()
        {
            return await Processor.LoadAthleteInformation();
            
        }

        public static async Task<AthleteStats> loadAthleteStats(int athleteId)
        {
            return await Processor.LoadAthleteStatsInfo(athleteId);
        }

        public static async Task<AuthorizationModel> loadAuthorization(string code)
        {
            return await Processor.Authorization(code);
        }
    }
}