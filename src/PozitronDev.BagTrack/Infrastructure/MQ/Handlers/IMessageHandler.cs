namespace PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

public interface IMessageHandler
{
    Task<bool> HandleAsync(BagTrackDbContext dbContext, string data, CancellationToken cancellationToken);
}
