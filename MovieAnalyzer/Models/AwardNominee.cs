using System.ComponentModel.DataAnnotations;

namespace MovieAnalyzer.Models
{
	public class AwardNominee
	{
		[Key]
		public string Title { get; set; }
		public int Year { get; set; }
		public string Studios { get; set; }
		public string Producers { get; set; }
		public bool Winner { get; set; }
	}
}
