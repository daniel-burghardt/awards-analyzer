using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MovieAnalyzer.Models;

namespace MovieAnalyzer.Tests
{
	class MovieAnalyzerApplication : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			var root = new InMemoryDatabaseRoot();

			builder.ConfigureServices(services =>
			{
				services.RemoveAll(typeof(DbContextOptions<MovieAnalyzerContext>));
				services.AddDbContext<MovieAnalyzerContext>(options =>
					options.UseInMemoryDatabase("TestDatabase", root));
			});

			return base.CreateHost(builder);
		}
	}
}
