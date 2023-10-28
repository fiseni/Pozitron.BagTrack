using Hangfire.Server;
using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class CleanInboxMessagesJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTime _dateTime;
    private readonly JobSettings _jobSettings;

    public CleanInboxMessagesJob(IServiceScopeFactory serviceScopeFactory,
                        IDateTime dateTime,
                        JobSettings jobSettings)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _dateTime = dateTime;
        _jobSettings = jobSettings;
    }

    public async Task Start(PerformContext context, CancellationToken cancellationToken)
    {
        var date = _dateTime.UtcNow.AddDays(-1 * _jobSettings.CleanOlderThanDays);

        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BagTrackDbContext>();

        await dbContext.InboxMessages
            .Where(x => x.CreatedDate < date)
            .ExecuteDeleteAsync();
    }
}
