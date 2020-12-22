using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FitBananas.Models
{
    public class NewChallengeViewModel
    {
        public List<Challenge> AllChallenges { get; set; }
        public NewChallengeViewModel(BananaContext context, int athleteId)
        {
            AllChallenges = context.Challenges
                .Include(challenge => challenge.Athletes)
                    .ThenInclude(athlete => athlete.ThisAthlete)
                .ToList();
        }
    }
}