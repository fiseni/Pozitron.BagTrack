namespace PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

public class BaggageClaimMessageHandler : IMessageHandler
{
    public Task Handle(BagTrackDbContext dbContext, string data, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
