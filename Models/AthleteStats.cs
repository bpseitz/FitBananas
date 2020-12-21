using System.ComponentModel.DataAnnotations;
using System;
namespace FitBananas.Models
{
    public class AthleteStats
    {
        [Key]
        public int AthleteStatsId { get; set; }

        public int BikeTotalId { get; set; }
        public BikeTotal YTD_Ride_Totals { get; set; }
        public int RunTotalId { get; set; }
        public RunTotal YTD_Run_Totals { get; set; }

        public int SwimTotalId { get; set; }
        public SwimTotal YTD_Swim_Totals { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int AthleteId { get; set; }
        public Athlete User { get; set; }

    }
}