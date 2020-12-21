using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FitBananas.Models
{
    public class Athlete
    {
        [Key]
        public int AthleteId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public AthleteStats AthletesStats { get; set; }

        public List<AthleteChallenge> Challenges { get; set; }

    }
}