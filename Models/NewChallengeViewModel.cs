using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace FitBananas.Models
{
    public class NewChallengeViewModel
    {
        public List<Challenge> AllChallenges { get; set; }
        public int AthleteId { get; set; }

        public string JsonChallenges { get; set; }
        public NewChallengeViewModel(BananaContext context, int athleteId)
        {
            AllChallenges = context.Challenges
                .Include(challenge => challenge.Athletes)
                    .ThenInclude(athlete => athlete.ThisAthlete)
                .ToList();

            JsonChallenges = JsonConvert.SerializeObject(AllChallenges);
            AthleteId = athleteId;
        }
    }
}