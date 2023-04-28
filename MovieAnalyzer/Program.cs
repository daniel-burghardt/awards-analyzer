using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAnalyzer;
using MovieAnalyzer.Models;
using MovieAnalyzer.Repositories;
using MovieAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieAnalyzerContext>(options => options.UseInMemoryDatabase("MovieAnalyzerDb"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<AwardNomineeRepository>();
builder.Services.AddTransient<AwardNomineeService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var scopedContext = scope.ServiceProvider.GetRequiredService<MovieAnalyzerContext>();
	var scopedMapper = scope.ServiceProvider.GetRequiredService<IMapper>();
	DbSeeder.Initialize(scopedContext, scopedMapper, "movielist.csv");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
