using Microsoft.EntityFrameworkCore;
using MovieAnalyzer.Models;
using System.Data;

namespace MovieAnalyzer.Repositories
{
    public class AwardNominationRepository
	{
		private readonly MovieAnalyzerContext db;

		public AwardNominationRepository(MovieAnalyzerContext db)
		{
			this.db = db;
		}

		public async Task<List<ProducerHasNomination>> GetWinningNominations()
		{
			return await db.ProducerHasNominations
				.Where(x => x.AwardNomination.Winner)
				.Include(x => x.Producer)
				.Include(x => x.AwardNomination)
				.ToListAsync();
		}
	}
}
