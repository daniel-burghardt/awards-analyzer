using MovieAnalyzer.DTOs;
using MovieAnalyzer.Repositories;

namespace MovieAnalyzer.Services
{
	public class AwardNomineeService
	{
		private readonly AwardNomineeRepository repository;

		public AwardNomineeService(AwardNomineeRepository repository)
		{
			this.repository = repository;
		}

		public async Task<ExtremeIntervalsDto> GetExtremeIntervalsAsync()
		{
			var result = new ExtremeIntervalsDto()
			{
				Min = new List<ExtremeIntervalsEntryDto>(),
				Max = new List<ExtremeIntervalsEntryDto>(),
			};

			// Get winning award nominees with at least 2 wins
			var awardNominees = await repository.GetWinningNominees(minWins: 2);
			if (awardNominees.Count == 0)
				return result;
			if (awardNominees.Count < 2)
				throw new Exception("Unexpected number of award nominees returned");

			// Map min and max winning intervals for each producer
			var producersWithIntervals = awardNominees
				.GroupBy(a => a.Producers)
				.Select(g =>
				{
					var intervals = g.Zip(g.Skip(1), (a, b) => new ExtremeIntervalsEntryDto
					{
						Producer = g.Key,
						PreviousWin = a.Year,
						FollowingWin = b.Year,
						Interval = b.Year - a.Year,
					}).OrderBy(i => i.Interval);
					return new
					{
						Producers = g.Key,
						MinInterval = intervals.First(),
						MaxInterval = intervals.Last(),
					};
				});

			var maxIntervals = producersWithIntervals
				.GroupBy(x => x.MaxInterval.Interval)
				.OrderBy(x => x.Key)
				.Last()
				.Select(x => x.MaxInterval)
				.ToList();
			var minIntervals = producersWithIntervals
				.GroupBy(x => x.MinInterval.Interval)
				.OrderBy(x => x.Key)
				.First()
				.Select(x => x.MinInterval)
				.ToList();

			result.Max = maxIntervals!;
			result.Min = minIntervals!;
			
			return result;
		}
	}
}
