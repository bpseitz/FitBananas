using System.ComponentModel.DataAnnotations;
using System;

namespace FitBananas.Models
{
    public class BikeTotal
    {
        [Key]
        public int BikeTotalId { get; set; }
        public int Distance { get; set; }

        // we use the underscore to separate words,
        //because newtonsoft fits the data to the field name from the json package
        // and strava provides us with the data using underscores
        public int Elevation_Gain { get; set; }

        public DateTime CreatedAt{ get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // foreign key to athlete stats
        public int AthleteStatsId { get; set; }

        public AthleteStats UserStats { get; set; }

        public int DistanceToMiles()
        {
            return (int)(Distance / 1609.34);
        }

        public int ElevationGainToFeet()
        {
            return (int)(Elevation_Gain*3.28084);
        }
    }
}