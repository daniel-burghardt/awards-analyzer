using CsvHelper;
using MovieAnalyzer.Models;
using System.Globalization;
using MovieAnalyzer.DTOs;
using CsvHelper.Configuration;
using AutoMapper;
using System.Text.RegularExpressions;

namespace MovieAnalyzer
{
    public static class DbSeeder
	{
		public static void Initialize(MovieAnalyzerContext db, IMapper mapper, string filename)
		{
			db.Database.EnsureCreated();

			var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
			using (var reader = new StreamReader(filename))
			using (var csv = new CsvReader(reader, config))
			{
				var records = csv.GetRecords<AwardNomineeCsvEntry>().ToList();
				var uniqueProducers = records.Select(x => x.Producers)
					.SelectMany(x => Regex.Split(x, @",\s|\sand\s"))
					.Distinct()
					.Select(x => new Producer { Name = x })
					.ToList();
				var awardNominations = records.Select(record =>
				{
					var awardNomination = mapper.Map<AwardNomination>(record);
					awardNomination.HasProducers = uniqueProducers
						.Where(p => record.Producers.Contains(p.Name))
						.Select(p => new ProducerHasNomination
						{
							Producer = p
						}).ToList();

					return awardNomination;
				}).ToList();

				db.Producers.AddRange(uniqueProducers);
				db.AwardNominations.AddRange(awardNominations);
			}
			
			db.SaveChanges();
		}
	}
}
