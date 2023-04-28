using System.Text.Json.Serialization;

namespace MovieAnalyzer.DTOs
{
	public class ExtremeIntervalsDto
	{
		[JsonPropertyName("min")]
		public List<ExtremeIntervalsEntryDto> Min { get; set; } = new List<ExtremeIntervalsEntryDto>();
		
		[JsonPropertyName("max")]
		public List<ExtremeIntervalsEntryDto> Max { get; set; } = new List<ExtremeIntervalsEntryDto>();
	}

	public class ExtremeIntervalsEntryDto
	{
		[JsonPropertyName("producer")]
		public string Producer { get; set; }
		
		[JsonPropertyName("interval")]
		public int Interval { get; set; }
		
		[JsonPropertyName("previousWin")]
		public int PreviousWin { get; set; }
		
		[JsonPropertyName("followingWin")]
		public int FollowingWin { get; set; }
	}
}
