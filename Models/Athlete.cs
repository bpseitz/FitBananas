using System.ComponentModel.DataAnnotations;

namespace FitBananas.Models
{
    public class Athlete
    {
        [Key]
        public int AthleteId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
    }
}