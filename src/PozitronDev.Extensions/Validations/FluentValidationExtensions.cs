using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace PozitronDev.Extensions.Validations;

public static class FluentValidationExtensions
{
    public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddValidatorsFromAssembly(typeof(FluentValidationExtensions).Assembly);
        return services;
    }
}
