using PozitronDev.BagTrack.Api.Bags;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class ConfigurationReloadJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ConfigurationReloadJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(2000, stoppingToken);

        var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new ConfigurationReloadRequest(), stoppingToken);
    }
}
