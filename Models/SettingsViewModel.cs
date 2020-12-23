using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FitBananas.Models
{
    public class SettingsViewModel
    {
        public Athlete currentAthlete { get; set; }
        public SettingsViewModel(BananaContext context, int id)
        {
            currentAthlete = context.Athletes
                .Include(athlete => athlete.Challenges)
                    .ThenInclude(challenge => challenge.ThisChallenge)
                .FirstOrDefault(athlete => athlete.AthleteId == id);
        }
    }
}