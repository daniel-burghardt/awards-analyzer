using Microsoft.AspNetCore.Mvc;
using MovieAnalyzer.Services;

namespace MovieAnalyzer.Controllers
{
	[ApiController]
	[Route("[controller]s")]
	public class AwardNomineeController : ControllerBase
	{
		private readonly AwardNomineeService service;

		public AwardNomineeController(AwardNomineeService service)
		{
			this.service = service;
		}

		[HttpGet("Intervals")]
		public async Task<IActionResult> GetIntervals()
		{
			var extremeIntervals = await service.GetExtremeIntervalsAsync();

			return Ok(extremeIntervals);
		}
	}
}
