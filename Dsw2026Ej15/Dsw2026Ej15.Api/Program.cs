using Dsw2026Ej15.Api.Middleware;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IPersistence, PersistenceEF>();
            builder.Services.AddHealthChecks();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Crea la base de datos si todavía no existe
                context.Database.Migrate();

                // Solo carga el JSON si la tabla está vacía
                if (!context.Speacilities.Any())
                {
                    var jsonPath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Sources",
                        "specialities.json");

                    var json = File.ReadAllText(jsonPath);

                    var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                    if (specialities != null)
                    {
                        foreach (var s in specialities)
                        {
                            context.Speacilities.Add(
                                new Speciality(
                                    s.Name,
                                    s.Description,
                                    s.Id));
                        }

                        context.SaveChanges();
                    }
                }
            }

            app.UseMiddleware<ExceptionMiddleware>();
          

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.MapHealthChecks("/health-check");

            app.Run();
        }
    }
}
