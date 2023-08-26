using Microsoft.Extensions.DependencyInjection;
using PozitronDev.SharedKernel.Contracts;

namespace MediatR;

internal class AsyncNoWaitPublisher : INotificationPublisher
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly INotificationPublisher _notificationPublisher;

    public AsyncNoWaitPublisher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _notificationPublisher = new AsyncSequentialContinueOnExceptionPublisher();
    }

    public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        _ = Task.Run(async () =>
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetService<IAppLogger<ExtendedMediator>>();
            try
            {
                var mediator = new Mediator(scope.ServiceProvider, _notificationPublisher);
                await mediator.Publish(notification, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Error occured while executing the handler in NoWait mode!");
            }

        }, cancellationToken);

        return Task.CompletedTask;
    }
}
