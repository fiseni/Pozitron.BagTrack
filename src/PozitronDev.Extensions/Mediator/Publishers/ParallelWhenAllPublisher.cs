namespace MediatR;

internal class ParallelWhenAllPublisher : INotificationPublisher
{
    public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();

        foreach (var handlerExecutor in handlerExecutors)
        {
            tasks.Add(Task.Run(() => handlerExecutor.HandlerCallback(notification, cancellationToken), cancellationToken));
        }

        return Task.WhenAll(tasks);
    }
}
