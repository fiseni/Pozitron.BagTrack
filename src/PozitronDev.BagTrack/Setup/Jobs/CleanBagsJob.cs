using Hangfire.Server;
using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class CleanBagsJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTime _dateTime;
    private readonly JobSettings _jobSettings;

    public CleanBagsJob(IServiceScopeFactory serviceScopeFactory,
                        IDateTime dateTime,
                        JobSettings jobSettings)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _dateTime = dateTime;
        _jobSettings = jobSettings;
    }

    public async Task Start(PerformContext context, CancellationToken cancellationToken)
    {
        var date = _dateTime.UtcNow.AddDays(-1 * _jobSettings.CleanBagsOlderThanDays);

        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BagTrackDbContext>();

        await dbContext.Bags
            .Where(x => x.Date < date)
            .ExecuteDeleteAsync();
    }
}
