using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MovieAnalyzer.DTOs;
using MovieAnalyzer.Models;
using System.Net;
using System.Net.Http.Json;

namespace MovieAnalyzer.Tests
{
	public class Tests
	{
		private IMapper mapper;

		[SetUp]
		public void Setup()
		{
			// Setup AutoMapper
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutoMapperProfile>();
			});
			mapper = config.CreateMapper();
		}

		[Test]
		public async Task GetExtremeIntervalsTest_OriginalFile()
		{
			await using var application = new MovieAnalyzerApplication();

			var client = application.CreateClient();
			var response = await client.GetAsync("/award-nominees/intervals");
			var responseBody = await response.Content.ReadFromJsonAsync<ExtremeIntervalsDto>();
			var expectedResponse = new ExtremeIntervalsDto
			{
				Min = new List<ExtremeIntervalsEntryDto>() {
					new ExtremeIntervalsEntryDto()
					{
						Producer = "Joel Silver",
						Interval = 1,
						PreviousWin = 1990,
						FollowingWin = 1991
					},
				},
				Max = new List<ExtremeIntervalsEntryDto>() {
					new ExtremeIntervalsEntryDto()
					{
						Producer = "Matthew Vaughn",
						Interval = 13,
						PreviousWin = 2002,
						FollowingWin = 2015
					},
				}
			};
			
			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.IsNotNull(responseBody);
			Assert.True(Util.AreEqualByJson(responseBody, expectedResponse));
		}

		[Test]
		public async Task GetExtremeIntervalsTest_V2()
		{
			await using var application = new MovieAnalyzerApplication();
			await LoadDbFromFile(application, "TestFiles/movielist_v2.csv");

			var client = application.CreateClient();
			var response = await client.GetAsync("/award-nominees/intervals");
			var responseBody = await response.Content.ReadFromJsonAsync<ExtremeIntervalsDto>();

			var expectedResponse = new ExtremeIntervalsDto
			{
				Min = new List<ExtremeIntervalsEntryDto>()
				{
					new ExtremeIntervalsEntryDto()
					{
						Producer = "A",
						Interval = 1,
						PreviousWin = 1997,
						FollowingWin = 1998
					},
				},
				Max = new List<ExtremeIntervalsEntryDto>() {
					new ExtremeIntervalsEntryDto()
					{
						Producer = "B",
						Interval = 7,
						PreviousWin = 1991,
						FollowingWin = 1998
					},
				}
			};

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.IsNotNull(responseBody);
			Assert.True(Util.AreEqualByJson(responseBody, expectedResponse));
		}

		[Test]
		public async Task GetExtremeIntervalsTest_EmptyFile()
		{
			await using var application = new MovieAnalyzerApplication();
			await LoadDbFromFile(application, "TestFiles/movielist_empty.csv");

			var client = application.CreateClient();
			var response = await client.GetAsync("/award-nominees/intervals");
			var responseBody = await response.Content.ReadFromJsonAsync<ExtremeIntervalsDto>();

			var expectedResponse = new ExtremeIntervalsDto
			{
				Min = new List<ExtremeIntervalsEntryDto>(),
				Max = new List<ExtremeIntervalsEntryDto>(),
			};

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.IsNotNull(responseBody);
			Assert.True(Util.AreEqualByJson(responseBody, expectedResponse));
		}

		[Test]
		public async Task GetExtremeIntervalsTest_NoConsecutiveWinners()
		{
			await using var application = new MovieAnalyzerApplication();
			await LoadDbFromFile(application, "TestFiles/movielist_no_consec.csv");

			var client = application.CreateClient();
			var response = await client.GetAsync("/award-nominees/intervals");
			var responseBody = await response.Content.ReadFromJsonAsync<ExtremeIntervalsDto>();

			var expectedResponse = new ExtremeIntervalsDto
			{
				Min = new List<ExtremeIntervalsEntryDto>(),
				Max = new List<ExtremeIntervalsEntryDto>(),
			};

			Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			Assert.IsNotNull(responseBody);
			Assert.True(Util.AreEqualByJson(responseBody, expectedResponse));
		}

		private async Task LoadDbFromFile(MovieAnalyzerApplication application, string filename)
		{
			using (var scope = application.Services.CreateScope())
			{
				var provider = scope.ServiceProvider;
				using (var dbContext = provider.GetRequiredService<MovieAnalyzerContext>())
				{
					await dbContext.Database.EnsureDeletedAsync();
					DbSeeder.Initialize(dbContext, mapper, filename);
				}
			}
		}
	}
}