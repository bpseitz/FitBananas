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
            Console.WriteLine($"Exchange Token State: {state}");
            Console.WriteLine($"Exchange Token Code: {code}");
            Console.WriteLine($"Exchange Token Scope: {scope}");

            // send post request to strava api with code
            // to receive the access token and refresh token
            AuthorizationModel authModel =  loadAuthorization(code).Result;

            // Extract token info from authModel
            var newToken = new Token();
            newToken.ExpiresAt = DateTime.Now.AddSeconds(authModel.Expires_In);
            newToken.ExpiresIn = TimeSpan.FromSeconds(authModel.Expires_In);
            newToken.RefreshToken = authModel.Refresh_Token;
            newToken.AccessToken = authModel.Access_Token;
            
            // Query database for athlete that has a matching Strava Id
            Athlete dbAthlete = _context.Athletes
                .FirstOrDefault(athlete => athlete.Id == authModel.Athlete.Id);
            int athleteId;
            if (dbAthlete == null)
            {
                athleteId = CreateAthlete(authModel.Athlete, newToken);
            }
            else
            {
                athleteId = dbAthlete.AthleteId;
                Token dbToken = _context.Tokens
                    .FirstOrDefault(token => token.TokenId == dbAthlete.TokenId);
                dbToken.ExpiresAt = newToken.ExpiresAt;
                dbToken.ExpiresIn = newToken.ExpiresIn;
                dbToken.RefreshToken = newToken.RefreshToken;
                dbToken.AccessToken = newToken.AccessToken;
                _context.SaveChanges();
            }
            HttpContext.Session.SetInt32("AthleteId", athleteId);
        
            return RedirectToAction("Home", "Banana");
        }

        public int CreateAthlete(Athlete newAthlete, Token newToken)
        {
            _context.Add(newToken);
            _context.SaveChanges();
            newAthlete.TokenId = newToken.TokenId;
            
            _context.Add(newAthlete);
            _context.SaveChanges();
            
            var newAthleteStats = new AthleteStats();
            newAthleteStats.AthleteId = newAthlete.AthleteId;
            _context.Add(newAthleteStats);
            _context.SaveChanges();

            int newAthleteStatsId = newAthleteStats.AthleteStatsId;
            var newBikeTotal = new BikeTotal();
            newBikeTotal.AthleteStatsId = newAthleteStatsId;
            _context.Add(newBikeTotal);
            var newRunTotal = new RunTotal();
            newRunTotal.AthleteStatsId = newAthleteStatsId;
            _context.Add(newRunTotal);
            var newSwimTotal = new SwimTotal();
            newSwimTotal.AthleteStatsId = newAthleteStatsId;
            _context.Add(newSwimTotal);
            _context.SaveChanges();

            return newAthlete.AthleteId;
        }

        public static async Task<AthleteStats> loadAthleteStats(int athleteId, string accessToken)
        {
            return await Processor.LoadAthleteStatsInfo(athleteId, accessToken);
        }

        public static async Task<AuthorizationModel> loadAuthorization(string code)
        {
            return await Processor.Authorization(code);
        }
        
        public static async Task<AuthorizationModel> loadNewToken(string refreshToken)
        {
            return await Processor.RefreshExpiredToken(refreshToken);
        }
    }
}