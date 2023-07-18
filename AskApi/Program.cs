
using AskApi.Services;
using HKDB.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
var HKDB = builder.Configuration.GetConnectionString("HKDB");

builder.Services.AddDbContext<HKContext>(options =>
				options.UseSqlServer(HKDB)
);

builder.Services.AddScoped<GptService>();
builder.Services.AddScoped<TranslateService>();
builder.Services.AddScoped<SimService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

