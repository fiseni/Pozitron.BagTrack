using Microsoft.EntityFrameworkCore;
using PozitronDev.BagTrack.Infrastructure.Seeds;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.CommissionPayment.Infrastructure;

public class BagTrackDbInitializer
{
    private readonly BagTrackDbContext _dbContext;
    private readonly IAppLogger<BagTrackDbInitializer> _logger;

    public BagTrackDbInitializer(
        BagTrackDbContext dbContext,
        IAppLogger<BagTrackDbInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        var hasDevices = await _dbContext.Devices.AnyAsync();

        if (!hasDevices)
        {
            var devices = DeviceSeed.Get();
            _dbContext.Devices.AddRange(devices);
            await _dbContext.SaveChangesAsync();
        }
    }
}
