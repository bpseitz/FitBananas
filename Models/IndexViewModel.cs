using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace FitBananas.Models
{
    public class IndexViewModel
    {
        public int Participants { get; set; }
        public int GoalsSet { get; set; }
        public int DistPledged { get; set;}
        public int AvgDistPledged { get; set; }
        public int ElevGainPledged { get; set; }
        public int AvgElevGainPledged { get; set; }
        public int DistanceCompleted { get; set; }
        public int ElevationGainCompleted { get; set; }

        public IndexViewModel(BananaContext context)
        {
            Participants = context.Athletes.ToList().Count;

            var allGoals = context.AthleteChallenges
                .Include(goal => goal.ThisChallenge)
                .ToList();

            GoalsSet = allGoals.Count;

            DistPledged = ToMiles(allGoals.Select(goal => goal.ThisChallenge)
                .Where(challenge => challenge.ChallengeType == "Distance")
                .Select(challenge => challenge.Goal)
                .Sum());



            AvgDistPledged = DistPledged / Participants;

            ElevGainPledged = allGoals.Select(goal => goal.ThisChallenge)
                .Where(challenge => challenge.ChallengeType == "Elevation Gain")
                .Select(challenge => challenge.Goal)
                .Sum();

            AvgElevGainPledged = ElevGainPledged / Participants;

            var bikeDistanceCompleted = context.BikeTotals
                .Select(total => total.Distance).Sum();

            var runDistanceCompleted = context.RunTotals
                .Select(total => total.Distance).Sum();

            var swimDistanceCompleted = context.SwimTotals
                .Select(total => total.Distance).Sum();

            var bikeElevGainCompleted = context.BikeTotals
                .Select(total => total.Elevation_Gain).Sum();

            var runElevGainCompleted = context.RunTotals
                .Select(total => total.Elevation_Gain).Sum();

            DistanceCompleted = ToMiles(bikeDistanceCompleted + runDistanceCompleted +
                swimDistanceCompleted);

            ElevationGainCompleted = (int)((bikeElevGainCompleted + runElevGainCompleted) * 3.28084);

            
        }

        public int ToMiles(int val)
        {
            return (int)(val / 1609.34);
        }
    }
}