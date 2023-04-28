using CsvHelper;
using MovieAnalyzer.Models;
using System.Globalization;
using MovieAnalyzer.DTOs;
using CsvHelper.Configuration;
using AutoMapper;

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
				var records = csv.GetRecords<AwardNomineeCsvEntry>();
				var awardNominees = mapper.Map<List<AwardNominee>>(records);
				db.AwardNominees.AddRange(awardNominees);
			}

			db.SaveChanges();
		}
	}
}
