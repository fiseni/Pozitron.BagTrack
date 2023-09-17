using IBM.WMQ;
using PozitronDev.BagTrack.Domain.Messaging;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public class MQSubscriberService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTime _dateTime;
    private readonly IAppLogger<MQSubscriberService> _logger;
    private readonly IMQAdapterService _mqAdapterService;

    public MQSubscriberService(
        IServiceScopeFactory serviceScopeFactory,
        IDateTime dateTime,
        IAppLogger<MQSubscriberService> logger,
        IMQAdapterService mqAdapterService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _dateTime = dateTime;
        _logger = logger;
        _mqAdapterService = mqAdapterService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await ListenForMessages(cancellationToken);
            await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
        }
    }

    private async Task ListenForMessages(CancellationToken cancellationToken)
    {
        await Task.Run(() => _mqAdapterService.ListenToTopic("BroadCast", MessageHandler, ConnectionStatusChangedHandler), cancellationToken);
    }

    private void ConnectionStatusChangedHandler(MQConnectionStatus connectionStatus)
    {
        _logger.LogInformation(connectionStatus.ToString());
    }

    private void MessageHandler(MQMessage mqMessage)
    {
        var msg = mqMessage.ReadString(mqMessage.DataLength);
        var message = new InboxMessage(_dateTime, msg);

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<BagTrackDbContext>();

            dbContext.InboxMessages.Add(message);
            dbContext.SaveChanges();
        }

        _logger.LogInformation(msg);
    }
}
