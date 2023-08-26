namespace MediatR;

internal class AsyncSequentialStopOnExceptionPublisher : INotificationPublisher
{
    public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        foreach (var handlerExecutor in handlerExecutors)
        {
            await handlerExecutor.HandlerCallback(notification, cancellationToken).ConfigureAwait(false);
        }
    }
}
