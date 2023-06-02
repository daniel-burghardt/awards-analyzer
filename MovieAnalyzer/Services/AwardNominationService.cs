using MovieAnalyzer.DTOs;
using MovieAnalyzer.Repositories;

namespace MovieAnalyzer.Services
{
	public class AwardNominationService
	{
		private readonly AwardNominationRepository repository;

		public AwardNominationService(AwardNominationRepository repository)
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

			// Get winning award nominees
			var winningNominations = await repository.GetWinningNominations();
			
			// Get producers with multiple wins
			var multipleWinning = winningNominations
				.OrderBy(x => x.AwardNomination.Year)
				.GroupBy(a => a.ProducerId)
				.Where(x => x.Count() >= 2).ToList();
			if (multipleWinning.Count == 0)
				return result;

			// Map min and max winning intervals for each producer
			var producersWithIntervals = multipleWinning
				.Select(g =>
				{
					var intervals = g.Zip(g.Skip(1), (a, b) => new ExtremeIntervalsEntryDto
					{
						Producer = a.Producer.Name,
						PreviousWin = a.AwardNomination.Year,
						FollowingWin = b.AwardNomination.Year,
						Interval = b.AwardNomination.Year - a.AwardNomination.Year,
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
