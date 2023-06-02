using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAnalyzer.Models
{
	public class ProducerHasNomination
	{
		public int ProducerId { get; set; }
		public int AwardNominationId { get; set; }

		[ForeignKey(nameof(ProducerId))]
		public Producer Producer { get; set; }

		[ForeignKey(nameof(AwardNominationId))]
		public AwardNomination AwardNomination { get; set; }
	}
}
