using Microsoft.EntityFrameworkCore;
using MovieAnalyzer.Models;
using System.Data;

namespace MovieAnalyzer.Repositories
{
    public class AwardNomineeRepository
	{
		private readonly MovieAnalyzerContext db;

		public AwardNomineeRepository(MovieAnalyzerContext db)
		{
			this.db = db;
		}

		public async Task<List<AwardNominee>> GetWinningNominees(int minWins = 0)
		{
			var producers = await db.AwardNominees
				.GroupBy(x => x.Producers)
				.Where(x => x.Count(z => z.Winner == true) >= minWins)
				.Select(x => x.Key)
				.ToListAsync();

			var awardNominees = await db.AwardNominees
				.Where(x => producers.Contains(x.Producers))
				.Where(x => x.Winner)
				.ToListAsync();

			return awardNominees;
		}
	}
}
