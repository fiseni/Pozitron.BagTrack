using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public record ConfigurationReloadRequest() : IRequest<Unit>;

public class ConfigurationReloadHandler : IRequestHandler<ConfigurationReloadRequest, Unit>
{
    private readonly BagTrackDbContext _dbContext;
    private readonly ICacheReloader<Device> _deviceCacheReloader;

    public ConfigurationReloadHandler(
        BagTrackDbContext dbContext,
        ICacheReloader<Device> deviceCacheReloader)
    {
        _dbContext = dbContext;
        _deviceCacheReloader = deviceCacheReloader;
    }

    public async Task<Unit> Handle(ConfigurationReloadRequest request, CancellationToken cancellationToken)
    {
        var devices = await _dbContext.Devices.ToListAsync(cancellationToken);

        _deviceCacheReloader.ReloadCache(devices);

        return Unit.Value;
    }
}
