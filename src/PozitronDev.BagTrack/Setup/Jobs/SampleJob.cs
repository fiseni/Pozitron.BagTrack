using Hangfire.Server;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class SampleJob
{
    public async Task Start(PerformContext context, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
