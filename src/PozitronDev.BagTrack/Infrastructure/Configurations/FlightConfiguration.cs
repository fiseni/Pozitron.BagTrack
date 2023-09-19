using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.Property(x => x.AirlineIATA).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.Number).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.NumberIATA).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.ActiveCarousel).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.AllocatedCarousel).HasMaxLength(10).IsUnicode(false);

        builder.HasIndex(x => x.AirlineIATA);
        builder.HasIndex(x => x.ActiveCarousel);
        builder.HasIndex(x => x.Start);
        builder.HasIndex(x => x.Stop);
    }
}
