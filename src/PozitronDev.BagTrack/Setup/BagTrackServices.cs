using Azure.Identity;
using BlazarTech.QueryableValues;
using Microsoft.EntityFrameworkCore;
using PozitronDev.CommissionPayment.Infrastructure;
using PozitronDev.Extensions.Automapper;
using PozitronDev.Extensions.Net;
using PozitronDev.Extensions.Validations;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Setup;

public static class BagTrackServices
{
    public static void BindConfigurations(this WebApplicationBuilder builder)
    {
        builder.Configuration.Bind(BagTrackSettings.CONFIG_NAME, BagTrackSettings.Instance);
        builder.Configuration.Bind(KeyVaultSettings.CONFIG_NAME, KeyVaultSettings.Instance);

        if (!KeyVaultSettings.Instance.DisableAzureKeyVault && !string.IsNullOrEmpty(KeyVaultSettings.Instance.AzureKeyVault))
        {
            builder.Configuration.AddAzureKeyVault(new Uri(KeyVaultSettings.Instance.AzureKeyVault), new DefaultAzureCredential());
        }
    }

    public static void AddBagTrackServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddExtendedMediatR(typeof(BagTrackMarker));
        builder.Services.AddCustomAutoMapper(typeof(BagTrackMarker).Assembly);
        builder.Services.AddCustomFluentValidation(typeof(BagTrackMarker).Assembly);

        var connectionString = KeyVaultSettings.Instance.DisableAzureKeyVault
            ? BagTrackSettings.Instance.ConnectionString
            : builder.Configuration.GetSection(BagTrackSettings.Instance.ConnectionString).Get<string>();

        builder.Services.AddDbContext<BagTrackDbContext>(options => options.UseSqlServer(connectionString, sqlServerOptions =>
        {
            sqlServerOptions.UseQueryableValues(config => config.Serialization(SqlServerSerialization.Auto));
        }));
        builder.Services.AddScoped<BagTrackDbInitializer>();

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    public static async Task Initialize(this WebApplication app)
    {
        if (app.Environment.IsAnyDevelopment())
        {
            await BagTrackDbInitializer.SeedAsync(app.Services);
        }
    }
}
