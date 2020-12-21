using Microsoft.EntityFrameworkCore;

namespace FitBananas.Models
{
    public class BananaContext : DbContext
    {
        public BananaContext(DbContextOptions options) : base(options) { }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<AthleteChallenge> AthleteChallenges { get; set; }
        public DbSet<AthleteStats> AthleteStatsSets { get; set; }
        public DbSet<BikeTotal> BikeTotals { get; set; }
        public DbSet<RunTotal> RunTotals { get; set; }
        public DbSet<SwimTotal> SwimTotals { get; set; }
    }
}