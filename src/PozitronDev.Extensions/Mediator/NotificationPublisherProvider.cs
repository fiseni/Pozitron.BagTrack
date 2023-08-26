using Microsoft.Extensions.DependencyInjection;

namespace MediatR;

internal class NotificationPublisherProvider : IPublisherProvider
{
    private static readonly Dictionary<PublishStrategy, INotificationPublisher> _publishers = new();

    public NotificationPublisherProvider(IServiceScopeFactory serviceScopeFactory)
    {
        _publishers.TryAdd(PublishStrategy.AsyncSequentialStopOnException, new AsyncSequentialStopOnExceptionPublisher());
        _publishers.TryAdd(PublishStrategy.AsyncSequentialContinueOnException, new AsyncSequentialContinueOnExceptionPublisher());
        _publishers.TryAdd(PublishStrategy.AsyncWhenAll, new AsyncWhenAllPublisher());
        _publishers.TryAdd(PublishStrategy.ParallelWhenAll, new ParallelWhenAllPublisher());
        _publishers.TryAdd(PublishStrategy.AsyncNoWait, new AsyncNoWaitPublisher(serviceScopeFactory));
        _publishers.TryAdd(PublishStrategy.ParallelNoWait, new ParallelNoWaitPublisher(serviceScopeFactory));
    }

    public INotificationPublisher GetPublisher(PublishStrategy publishStrategy)
    {
        if (_publishers.TryGetValue(publishStrategy, out var publisher)) return publisher;

        throw new NotSupportedException($"Unknown strategy: {publishStrategy}");
    }
}
