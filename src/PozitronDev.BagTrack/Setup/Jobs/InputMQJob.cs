using Hangfire.Server;

namespace PozitronDev.BagTrack.Setup.Jobs;

public class InputMQJob
{
    public async Task Start(PerformContext context, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
