using Microsoft.EntityFrameworkCore;
using PozitronDev.Extensions.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Infrastructure;

public class BagTrackDbContext : DbContext
{
    public DbSet<Bag> Bags => Set<Bag>();

    public BagTrackDbContext(DbContextOptions<BagTrackDbContext> options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.ConfigureCustomConventions();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ConfigureCustomRules(this, typeof(BagTrackMarker).Assembly);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // The order is important. Apply soft delete then auditing.
        this.ApplySoftDelete();
        //this.ApplyAuditing(_dateTime, _currentUser);

        var result = await base.SaveChangesAsync(cancellationToken);

        //await this.PublishDomainEvents(_mediator);

        return result;
    }
}
