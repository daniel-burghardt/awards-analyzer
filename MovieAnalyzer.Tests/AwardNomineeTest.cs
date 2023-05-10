using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAnalyzer.DTOs;
using MovieAnalyzer.Models;
using MovieAnalyzer.Repositories;
using MovieAnalyzer.Services;

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
			var db = InitializeDatabase("TestFiles/movielist.csv");
			var repository = new AwardNomineeRepository(db);
			var service = new AwardNomineeService(repository);

			var result = await service.GetExtremeIntervalsAsync();

			var expectedMinNominees = new List<ExtremeIntervalsEntryDto>() {
				new ExtremeIntervalsEntryDto()
				{
					Producer = "Bo Derek",
					Interval = 6,
					PreviousWin = 1984,
					FollowingWin = 1990
				},
			};
			var expectedMaxNominees = new List<ExtremeIntervalsEntryDto>() {
				new ExtremeIntervalsEntryDto()
				{
					Producer = "Bo Derek",
					Interval = 6,
					PreviousWin = 1984,
					FollowingWin = 1990
				},
			};

			Assert.True(Util.AreEqualByJson(expectedMinNominees, result.Min));
			Assert.True(Util.AreEqualByJson(expectedMaxNominees, result.Max));

			db.Database.EnsureDeleted();
		}

		[Test]
		public async Task GetExtremeIntervalsTest_V2()
		{
			var db = InitializeDatabase("TestFiles/movielist_v1.csv");
			var repository = new AwardNomineeRepository(db);
			var service = new AwardNomineeService(repository);

			var result = await service.GetExtremeIntervalsAsync();

			var expectedMinNominees = new List<ExtremeIntervalsEntryDto>() {
				new ExtremeIntervalsEntryDto()
				{
					Producer = "A",
					Interval = 1,
					PreviousWin = 1990,
					FollowingWin = 1991
				},
				new ExtremeIntervalsEntryDto()
				{
					Producer = "C",
					Interval = 1,
					PreviousWin = 1993,
					FollowingWin = 1994
				},
			};
			var expectedMaxNominees = new List<ExtremeIntervalsEntryDto>() {
				new ExtremeIntervalsEntryDto()
				{
					Producer = "B",
					Interval = 8,
					PreviousWin = 1992,
					FollowingWin = 2000
				},
			};

			Assert.True(Util.AreEqualByJson(expectedMinNominees, result.Min));
			Assert.True(Util.AreEqualByJson(expectedMaxNominees, result.Max));

			db.Database.EnsureDeleted();
		}

		[Test]
		public async Task GetExtremeIntervalsTest_EmptyFile()
		{
			var db = InitializeDatabase("TestFiles/movielist_empty.csv");
			var repository = new AwardNomineeRepository(db);
			var service = new AwardNomineeService(repository);

			var result = await service.GetExtremeIntervalsAsync();

			var expectedMinNominees = new List<ExtremeIntervalsEntryDto>();
			var expectedMaxNominees = new List<ExtremeIntervalsEntryDto>();

			Assert.True(Util.AreEqualByJson(expectedMinNominees, result.Min));
			Assert.True(Util.AreEqualByJson(expectedMaxNominees, result.Max));

			db.Database.EnsureDeleted();
		}

		[Test]
		public async Task GetExtremeIntervalsTest_NoConsecutiveWinners()
		{
			var db = InitializeDatabase("TestFiles/movielist_no_consec.csv");
			var repository = new AwardNomineeRepository(db);
			var service = new AwardNomineeService(repository);

			var result = await service.GetExtremeIntervalsAsync();

			var expectedMinNominees = new List<ExtremeIntervalsEntryDto>();
			var expectedMaxNominees = new List<ExtremeIntervalsEntryDto>();

			Assert.True(Util.AreEqualByJson(expectedMinNominees, result.Min));
			Assert.True(Util.AreEqualByJson(expectedMaxNominees, result.Max));

			db.Database.EnsureDeleted();
		}

		private MovieAnalyzerContext InitializeDatabase(string filename)
		{
			// Setup database
			var options = new DbContextOptionsBuilder<MovieAnalyzerContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;
			var db = new MovieAnalyzerContext(options);
			DbSeeder.Initialize(db, mapper, filename);

			return db;
		}
	}
}