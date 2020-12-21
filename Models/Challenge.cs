using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FitBananas.Models
{
    public class Challenge
    {
        [Key]
        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string ActivityType { get; set; }

        public string ChallengeType { get; set; }

        public int Goal { get; set; }

        public string ImageFileName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<AthleteChallenge> Athletes { get; set; }
    }
}