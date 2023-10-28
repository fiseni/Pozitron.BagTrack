using Microsoft.EntityFrameworkCore;
using PozitronDev.BagTrack.Domain.Messaging;
using PozitronDev.Extensions.EntityFrameworkCore;

namespace PozitronDev.BagTrack.Infrastructure;

public class BagTrackDbContext : DbContext
{
    public DbSet<Bag> Bags => Set<Bag>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Airline> Airlines => Set<Airline>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public BagTrackDbContext(DbContextOptions<BagTrackDbContext> options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        => configurationBuilder.ConfigureCustomConventions();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ConfigureCustomRules(this, typeof(BagTrackMarker).Assembly);
}
