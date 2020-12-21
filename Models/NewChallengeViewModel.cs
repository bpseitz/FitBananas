using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FitBananas.Models
{
    public class NewChallengeViewModel
    {
        public List<Challenge> allChallenges { get; set; }
        public NewChallengeViewModel(BananaContext context, int athleteId)
        {
            allChallenges = context.Challenges
                .Include(challenge => challenge.Athletes)
                    .ThenInclude(athlete => athlete.ThisAthlete)
                .ToList();
        }
    }
}