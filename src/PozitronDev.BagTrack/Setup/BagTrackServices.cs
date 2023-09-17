using Azure.Identity;
using BlazarTech.QueryableValues;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PozitronDev.BagTrack.Setup.Jobs;
using PozitronDev.BagTrack.Setup.Middleware;
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

        builder.Services.AddOptions<JobSettings>()
            .BindConfiguration(JobSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<BagTrackSettings>>().Value);
        builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<KeyVaultSettings>>().Value);
        builder.Services.AddSingleton(x => x.GetRequiredService<IOptions<JobSettings>>().Value);

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

        var options = new SqlServerStorageOptions
        {
            PrepareSchemaIfNecessary = false,
            EnableHeavyMigrations = false,
        };
        builder.Services.AddHangfire(x => x.UseSqlServerStorage(connectionString, options).UseConsole());
        builder.Services.AddHangfireServer();

        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddSingleton<DeviceCache>();
        builder.Services.AddSingleton<IDeviceCache, DeviceCache>(x => x.GetRequiredService<DeviceCache>());
        builder.Services.AddSingleton<ICacheReloader<Device>, DeviceCache>(x => x.GetRequiredService<DeviceCache>());
    }

    public static IApplicationBuilder UseHangfire(this WebApplication app)
    {
        var jobSettings = new JobSettings();
        app.Configuration.Bind(JobSettings.SECTION_NAME, jobSettings);

        if (!app.Environment.IsDevelopment() && jobSettings.DashboardUsername is not null && jobSettings.DashboardPassword is not null)
        {
            var options = new DashboardOptions
            {
                Authorization = new IDashboardAuthorizationFilter[]
                {
                    new BasicAuthAuthorizationFilter(
                        new BasicAuthAuthorizationFilterOptions
                        {
                            LoginCaseSensitive = false,
                            Users = new[]
                            {
                                new BasicAuthAuthorizationUser
                                {
                                    Login = jobSettings.DashboardUsername,
                                    PasswordClear = jobSettings.DashboardPassword
                                }
                            }
                        })
                }
            };
            app.UseHangfireDashboard("/jobs", options);
        }
        else
        {
            app.UseHangfireDashboard("/jobs");

        }

        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 0 });
        GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());

        RecurringJob.AddOrUpdate<InputMQJob>(nameof(InputMQJob), job => job.Start(null!, CancellationToken.None), jobSettings.InputMQJob);

        return app;

    }
}
