using System.ComponentModel.DataAnnotations;
using System;

namespace FitBananas.Models
{
    public class AthleteChallenge
    {
        [Key]
        public int AthleteChallengeId { get; set; }

        public int AthleteId { get; set; }
        public Athlete ThisAthlete { get; set; }

        public int ChallengeId { get; set; }
        public Challenge ThisChallenge { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}