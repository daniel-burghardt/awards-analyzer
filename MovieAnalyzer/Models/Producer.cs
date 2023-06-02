using System.ComponentModel.DataAnnotations;

namespace MovieAnalyzer.Models
{
	public class Producer
	{
		[Key]
		public int ProducerId { get; set; }
		public string Name { get; set; }
		public List<ProducerHasNomination> HasNominations { get; set; }
	}
}
