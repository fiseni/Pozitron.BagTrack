namespace PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

public interface IMessageHandler
{
    Task Handle(BagTrackDbContext dbContext, string data, CancellationToken cancellationToken);
}
