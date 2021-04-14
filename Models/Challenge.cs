using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FitBananas.Models
{
    public class Challenge
    {
        [Key]
        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string ActivityType { get; set; }

        // options: Swim, Bike, Run
        public string ChallengeType { get; set; }

        // in meters
        public int Goal { get; set; }

        public string ImageFileName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // This comes from Newtonsoft and allows us to prevent a referencing loop when serializing
        // We need to come up with a better fix for this, we could create a view model specific to challenges and Athletes I believe - TD
        [JsonIgnore]
        public List<AthleteChallenge> Athletes { get; set; }

        public int GoalToImperial()
        {
            // convert to feet
            if(ChallengeType == "Elevation Gain")
            {
                return (int)(Goal * 3.28084);
            }
            // convert to miles
            else
            {
                return (int)(Goal/1609.34);
            };
        }
    }
}