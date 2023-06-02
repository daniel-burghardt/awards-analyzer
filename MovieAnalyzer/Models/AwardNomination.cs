using System.ComponentModel.DataAnnotations;

namespace MovieAnalyzer.Models
{
	public class AwardNomination
	{
		[Key]
		public int AwardNominationId { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public string Studios { get; set; }
		public bool Winner { get; set; }

		public List<ProducerHasNomination> HasProducers { get; set; }
	}
}
