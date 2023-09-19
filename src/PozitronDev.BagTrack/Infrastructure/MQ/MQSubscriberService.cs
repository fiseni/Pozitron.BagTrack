using PozitronDev.BagTrack.Domain.Messaging;
using PozitronDev.BagTrack.Infrastructure.MQ.Handlers;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public class MQSubscriberService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDateTime _dateTime;
    private readonly IAppLogger<MQSubscriberService> _logger;
    private readonly IMQAdapterService _mqAdapterService;
    private readonly IMessageHandler _messageHandler;
    private readonly MQSettings _mQSettings;

    public MQSubscriberService(
        IServiceScopeFactory serviceScopeFactory,
        IDateTime dateTime,
        IAppLogger<MQSubscriberService> logger,
        IMQAdapterService mqAdapterService,
        IMessageHandler messageHandler,
        MQSettings mQSettings)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _dateTime = dateTime;
        _logger = logger;
        _mqAdapterService = mqAdapterService;
        _messageHandler = messageHandler;
        _mQSettings = mQSettings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (string.IsNullOrEmpty(_mQSettings.InputQueue)) return;

        await Task.Delay(5000, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await _mqAdapterService.ListenToQueue(_mQSettings.InputQueue, MessageHandler, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Error occurred. Retrying in 5 minutes");
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    public async Task MessageHandler(string data, CancellationToken cancellationToken)
    {
        _logger.LogInformation(data);

        var inboxMessage = new InboxMessage(_dateTime, data);

        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<BagTrackDbContext>();

            dbContext.InboxMessages.Add(inboxMessage);
            await dbContext.SaveChangesAsync(cancellationToken);

            if (await _messageHandler.HandleAsync(dbContext, data, cancellationToken))
            {
                inboxMessage.MarkAsProcessed(_dateTime);
            }
            else
            {
                _logger.LogError("The message was not parsed successfully!");
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
