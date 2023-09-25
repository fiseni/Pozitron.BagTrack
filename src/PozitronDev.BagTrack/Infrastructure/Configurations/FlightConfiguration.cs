using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.Property(x => x.AirlineIATA).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.Number).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.ActiveCarousel).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.AllocatedCarousel).HasMaxLength(10).IsUnicode(false);

        builder.HasIndex(x => new { x.AirlineIATA, x.ActiveCarousel, x.IsDeleted, x.Start, x.Stop })
            .IncludeProperties(x => x.Number);

        builder.HasIndex(x => new { x.AirlineIATA, x.Number, x.OriginDate, x.IsDeleted })
            .IncludeProperties(x => new { x.ActiveCarousel, x.AllocatedCarousel, x.Start, x.Stop });
    }
}
