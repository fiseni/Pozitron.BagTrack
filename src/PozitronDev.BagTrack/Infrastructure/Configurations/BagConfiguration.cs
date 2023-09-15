using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class BagConfiguration : IEntityTypeConfiguration<Bag>
{
    public void Configure(EntityTypeBuilder<Bag> builder)
    {
        builder.Property(x => x.BagTrackId).HasMaxLength(10);
        builder.Property(x => x.DeviceId).HasMaxLength(10);
        builder.Property(x => x.Carousel).HasMaxLength(10);
        builder.Property(x => x.Flight).HasMaxLength(50);
        builder.Property(x => x.IsResponseNeeded).HasMaxLength(1);
        builder.Property(x => x.JulianDate).HasMaxLength(10);
    }
}
