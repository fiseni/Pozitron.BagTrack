using PozitronDev.BagTrack.Api.Bags;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class ConfigurationReloadJob : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ConfigurationReloadJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Send(new ConfigurationReloadRequest(), cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
