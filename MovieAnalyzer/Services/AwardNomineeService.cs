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
			var producers = await repository.GetProducers(minWins: 2);
			var awardNominees = await repository.GetNomineesForProducers(producers);
			var result = new ExtremeIntervalsDto()
			{
				Min = new List<ExtremeIntervalsEntryDto>(),
				Max = new List<ExtremeIntervalsEntryDto>(),
			};

			int minInterval = int.MaxValue;
			int maxInterval = int.MinValue;
			// Loop over producers with at least two total wins
			foreach (var producer in producers)
			{
				// Loop over all but the last of the producer's nominations ordered by year
				// comparing with the subsequent nomination
				var producerNominations = awardNominees
					.Where(x => x.Producers == producer)
					.OrderBy(x => x.Year)
					.ToArray();

				for (var i = 0; i <= producerNominations.Count() - 2; i++)
				{
					// Check whether the producer has won two consecutive years
					if (producerNominations[i].Winner && producerNominations[i + 1].Winner)
					{
						var interval = producerNominations[i + 1].Year - producerNominations[i].Year;
						if (interval >= maxInterval)
						{
							var entry = new ExtremeIntervalsEntryDto()
							{
								Producer = producer,
								Interval = interval,
								PreviousWin = producerNominations[i].Year,
								FollowingWin = producerNominations[i + 1].Year
							};

							if (interval == maxInterval)
								result.Max.Add(entry);
							else
							{
								result.Max = new List<ExtremeIntervalsEntryDto> { entry };
								maxInterval = interval;
							}
						}

						if (interval <= minInterval)
						{
							var entry = new ExtremeIntervalsEntryDto()
							{
								Producer = producer,
								Interval = interval,
								PreviousWin = producerNominations[i].Year,
								FollowingWin = producerNominations[i + 1].Year
							};

							if (interval == minInterval)
								result.Min.Add(entry);
							else
							{
								result.Min = new List<ExtremeIntervalsEntryDto> { entry };
								minInterval = interval;
							}
						}
					}
				}
			}

			return result;
		}
	}
}
