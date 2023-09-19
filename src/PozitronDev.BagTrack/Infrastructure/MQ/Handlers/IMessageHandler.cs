namespace PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

public interface IMessageHandler
{
    Task<bool> Handle(BagTrackDbContext dbContext, string data, CancellationToken cancellationToken);
}
