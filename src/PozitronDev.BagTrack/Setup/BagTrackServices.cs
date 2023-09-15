using Azure.Identity;
using BlazarTech.QueryableValues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PozitronDev.CommissionPayment.Infrastructure;
using PozitronDev.Extensions.Automapper;
using PozitronDev.Extensions.Validations;

namespace PozitronDev.BagTrack.Setup;

public static class BagTrackServices
{
    public static void AddBagTrackServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddExtendedMediatR(typeof(BagTrackMarker));
        builder.Services.AddCustomAutoMapper(typeof(BagTrackMarker).Assembly);
        builder.Services.AddCustomFluentValidation(typeof(BagTrackMarker).Assembly);

        builder.Services.AddOptions<BagTrackSettings>()
            .BindConfiguration(BagTrackSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddOptions<KeyVaultSettings>()
            .BindConfiguration(KeyVaultSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<BagTrackSettings>>().Value);
        builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<KeyVaultSettings>>().Value);

        var keyVaultSettings = new KeyVaultSettings();
        builder.Configuration.Bind(KeyVaultSettings.SECTION_NAME, keyVaultSettings);

        var bagTrackSettings = new BagTrackSettings();
        builder.Configuration.Bind(BagTrackSettings.SECTION_NAME, bagTrackSettings);

        if (!keyVaultSettings.DisableAzureKeyVault && !string.IsNullOrEmpty(keyVaultSettings.AzureKeyVault))
        {
            builder.Configuration.AddAzureKeyVault(new Uri(keyVaultSettings.AzureKeyVault), new DefaultAzureCredential());
        }

        var connectionString = keyVaultSettings.DisableAzureKeyVault
            ? bagTrackSettings.ConnectionString
            : builder.Configuration.GetValue<string>(bagTrackSettings.ConnectionString);

        builder.Services.AddDbContext<BagTrackDbContext>(options => options.UseSqlServer(connectionString, sqlServerOptions =>
        {
            sqlServerOptions.UseQueryableValues(config => config.Serialization(SqlServerSerialization.Auto));
        }));
        builder.Services.AddScoped<BagTrackDbInitializer>();

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddSingleton<DeviceCache>();
        builder.Services.AddSingleton<IDeviceCache, DeviceCache>(x => x.GetRequiredService<DeviceCache>());
        builder.Services.AddSingleton<ICacheReloader<Device>, DeviceCache>(x => x.GetRequiredService<DeviceCache>());
    }
}
