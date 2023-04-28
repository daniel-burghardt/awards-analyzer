using CsvHelper.Configuration.Attributes;

namespace MovieAnalyzer.DTOs
{
	public class AwardNomineeCsvEntry
	{
		[Name("title")]
		public string Title { get; set; }

		[Name("year")]
		public int Year { get; set; }

		[Name("studios")]
		public string Studios { get; set; }

		[Name("producers")]
		public string Producers { get; set; }

		[Name("winner")]
		[BooleanTrueValues("yes")]
		[BooleanFalseValues("")]
		public bool Winner { get; set; }
	}
}
