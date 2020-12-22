using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace FitBananas.Models
{
    public class HomeViewModel
    {
        public string AthleteName { get; set; }

        public AthleteStats CurrentStats { get; set; }
        public List<Challenge> AthleteChallenges { get; set; }
        public int DaysRemaining { get; set; }
        public HomeViewModel(BananaContext context, int stravaId)
        {
            // get the current athlete from the db
            Athlete dbAthlete = context.Athletes
                .Include(athlete => athlete.Challenges)
                    .ThenInclude(challenge => challenge.ThisChallenge)
                .FirstOrDefault(athlete => athlete.Id == stravaId);
                
            // get the stats of the athlete separately to get each stats
            CurrentStats = context.AthleteStatsSets
                .Include(stats => stats.YTD_Ride_Totals)
                .Include(stats => stats.YTD_Run_Totals)
                .Include(stats => stats.YTD_Swim_Totals)
                .FirstOrDefault(stats => stats.AthleteId == dbAthlete.AthleteId);

            // store the challenges in a list
            AthleteChallenges = dbAthlete.Challenges
                .Select(challenge => challenge.ThisChallenge)
                .ToList();
            
            // store the athletes name
            AthleteName = dbAthlete.FirstName;

            // calculate and store the number of days remaining in the year
            var endOfYear = new DateTime(2022,01,01);
            DaysRemaining = (endOfYear.Date - DateTime.Now.Date).Days;
        }
    }
}