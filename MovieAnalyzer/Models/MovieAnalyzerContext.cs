using Microsoft.EntityFrameworkCore;

namespace MovieAnalyzer.Models
{
    public class MovieAnalyzerContext : DbContext
    {
        public MovieAnalyzerContext(DbContextOptions<MovieAnalyzerContext> options) : base(options) { }

        public DbSet<AwardNomination> AwardNominations { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<ProducerHasNomination> ProducerHasNominations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Producer>().HasIndex(x => x.Name).IsUnique();
			modelBuilder.Entity<ProducerHasNomination>().HasKey(x => new { x.ProducerId, x.AwardNominationId });
		}
	}
}
