using Microsoft.Extensions.Hosting;

namespace PozitronDev.Extensions;

public static class EnvironmentExtensions
{
    public static readonly string Testing = nameof(Testing);

    public static bool IsTesting(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName.Equals(Testing);
    }

    public static bool IsAnyDevelopment(this IHostEnvironment hostEnvironment)
    {
        return hostEnvironment.EnvironmentName.Contains(Environments.Development);
    }
}
