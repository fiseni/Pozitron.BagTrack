using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace PozitronDev.BagTrack.Setup.Middleware;

public class ProlongExpirationTimeAttribute : JobFilterAttribute, IApplyStateFilter
{
    public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        context.JobExpirationTimeout = TimeSpan.FromDays(7);
    }

    public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
    {
        context.JobExpirationTimeout = TimeSpan.FromDays(7);
    }
}

