using Microsoft.EntityFrameworkCore;
using PozitronDev.Extensions.EntityFrameworkCore;
using PozitronDev.SharedKernel.Contracts;

namespace PozitronDev.BagTrack.Infrastructure;

public class BagTrackDbContext : DbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUser _currentUser;
    private readonly IMediator _mediator;

    public DbSet<Bag> Bags => Set<Bag>();

    public BagTrackDbContext(
        DbContextOptions<BagTrackDbContext> options,
        IDateTime dateTime,
        ICurrentUser currentUser,
        IMediator mediator) : base(options)
    {
        _dateTime = dateTime;
        _currentUser = currentUser;
        _mediator = mediator;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.ConfigureCustomConventions();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ConfigureCustomRules(this, typeof(BagTrackMarker).Assembly);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // The order is important. Apply soft delete then auditing.
        this.ApplySoftDelete();
        this.ApplyAuditing(_dateTime, _currentUser);

        var result = await base.SaveChangesAsync(cancellationToken);

        await this.PublishDomainEvents(_mediator);

        return result;
    }
}
