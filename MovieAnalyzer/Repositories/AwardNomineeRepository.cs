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

		public async Task<List<string>> GetProducers(int minWins = 0)
		{
			var producers = await db.AwardNominees
				.GroupBy(x => x.Producers)
				.Where(x => x.Count(z => z.Winner == true) >= minWins)
				.Select(x => x.Key)
				.ToListAsync();

			return producers;
		}
		
		public async Task<List<AwardNominee>> GetNomineesForProducers(List<string> producers)
		{
			var awardNominees = await db.AwardNominees
				.Where(x => producers.Contains(x.Producers))
				.ToListAsync();

			return awardNominees;
		}
	}
}
