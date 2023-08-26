using Microsoft.OpenApi.Models;

namespace PozitronDev.BagTrack.Setup;

public static class SwaggerExtensions
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Pozitron BagTrack API",
                    Version = "v1",
                    Description = "Entry points for the Pozitron BagTrack API"
                });

            c.EnableAnnotations();
        });
    }

    public static void UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pozitron BagTrack API");
            c.RoutePrefix = "api/documentation";
        });
    }
}
