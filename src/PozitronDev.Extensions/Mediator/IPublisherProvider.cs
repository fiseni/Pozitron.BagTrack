namespace MediatR;

public interface IPublisherProvider
{
    INotificationPublisher GetPublisher(PublishStrategy publishStrategy);
}
