using Microsoft.EntityFrameworkCore;

namespace MovieAnalyzer.Models
{
    public class MovieAnalyzerContext : DbContext
    {
        public MovieAnalyzerContext(DbContextOptions<MovieAnalyzerContext> options) : base(options) { }

        public DbSet<AwardNominee> AwardNominees { get; set; }
    }
}
