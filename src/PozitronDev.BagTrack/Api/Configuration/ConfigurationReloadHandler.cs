using Microsoft.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Api.Bags;

public record ConfigurationReloadRequest() : IRequest<Unit>;

public class ConfigurationReloadHandler : IRequestHandler<ConfigurationReloadRequest, Unit>
{
    private readonly BagTrackDbContext _dbContext;
    private readonly ICacheReloader _cacheReloader;

    public ConfigurationReloadHandler(
        BagTrackDbContext dbContext,
        ICacheReloader cacheReloader)
    {
        _dbContext = dbContext;
        _cacheReloader = cacheReloader;
    }

    public async Task<Unit> Handle(ConfigurationReloadRequest request, CancellationToken cancellationToken)
    {
        var devices = await _dbContext.Devices.AsNoTracking().ToListAsync(cancellationToken);
        _cacheReloader.ReloadDeviceCache(devices);

        var airlines = await _dbContext.Airlines.AsNoTracking().ToListAsync(cancellationToken);
        _cacheReloader.ReloadAirlineCache(airlines);

        return Unit.Value;
    }
}
