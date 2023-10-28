using PozitronDev.CommissionPayment.Infrastructure;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class DbInitializerJob : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DbInitializerJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<BagTrackDbInitializer>();

        await dbInitializer.SeedAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
