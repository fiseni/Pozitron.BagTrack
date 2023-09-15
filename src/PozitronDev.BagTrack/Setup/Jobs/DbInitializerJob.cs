using PozitronDev.CommissionPayment.Infrastructure;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class DbInitializerJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DbInitializerJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<BagTrackDbInitializer>();

        await dbInitializer.SeedAsync(stoppingToken);
    }
}
