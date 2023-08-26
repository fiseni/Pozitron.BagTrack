namespace MediatR;

public class ExtendedMediator : Mediator
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPublisherProvider _publisherProvider;

    public ExtendedMediator(IServiceProvider serviceProvider, IPublisherProvider publisherProvider)
        : base(serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _publisherProvider = publisherProvider;
    }

    public Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken)
        where TNotification : INotification
    {
        var serviceProvider = _serviceProvider;
        var publisher = _publisherProvider.GetPublisher(strategy);

        return new Mediator(serviceProvider, publisher).Publish(notification, cancellationToken);
    }
}
