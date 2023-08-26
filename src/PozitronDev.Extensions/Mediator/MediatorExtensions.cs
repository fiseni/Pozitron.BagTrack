using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR;

public static class MediatorExtensions
{
    public static Task Publish<TNotification>(this IMediator mediator, TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken)
        where TNotification : INotification
    {
        return mediator is ExtendedMediator customMediator
            ? customMediator.Publish(notification, strategy, cancellationToken)
            : throw new NotSupportedException("The custom mediator implementation is not registered!");
    }

    public static IServiceCollection AddExtendedMediatR(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.TryAdd(new ServiceDescriptor(typeof(IPublisherProvider), typeof(NotificationPublisherProvider), ServiceLifetime.Singleton));

        services.AddMediatR(options =>
        {
            options.MediatorImplementationType = typeof(ExtendedMediator);
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssemblies(assemblies);
        });
        return services;
    }

    public static IServiceCollection AddExtendedMediatR(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
    {
        var assemblies = handlerAssemblyMarkerTypes.Select(x=>x.Assembly).ToArray();

        services.TryAdd(new ServiceDescriptor(typeof(IPublisherProvider), typeof(NotificationPublisherProvider), ServiceLifetime.Singleton));

        services.AddMediatR(options =>
        {
            options.MediatorImplementationType = typeof(ExtendedMediator);
            options.Lifetime = ServiceLifetime.Scoped;
            options.RegisterServicesFromAssemblies(assemblies);
        });
        return services;
    }
}
