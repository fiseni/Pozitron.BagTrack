using PozitronDev.BagTrack.Infrastructure;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.CommissionPayment.Infrastructure;

public class BagTrackDbInitializer
{
    private readonly BagTrackDbContext _dbContext;
    private readonly IAppLogger<BagTrackDbInitializer> _logger;

    public BagTrackDbInitializer(BagTrackDbContext dbContext,
                                          IAppLogger<BagTrackDbInitializer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<BagTrackDbInitializer>();

            await initializer._dbContext.Database.EnsureCreatedAsync();
        }
    }
}
