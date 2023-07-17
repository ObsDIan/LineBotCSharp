using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace LineBot
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			//swagger
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "LineBot", Version = "v1" });
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			
			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}