using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PozitronDev.SharedKernel.Contracts;
using Serilog;

namespace PozitronDev.Extensions.Logging;
public static class LoggingConfiguration
{
    public static ILoggingBuilder AddLogging(this ILoggingBuilder loggingBuilder,
                                             IServiceCollection services,
                                             IConfiguration configuration,
                                             IHostEnvironment hostEnvironment)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog(logger);

        services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return loggingBuilder;

    }
}
