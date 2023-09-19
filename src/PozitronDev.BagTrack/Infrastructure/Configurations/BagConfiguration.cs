using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class BagConfiguration : IEntityTypeConfiguration<Bag>
{
    public void Configure(EntityTypeBuilder<Bag> builder)
    {
        builder.Property(x => x.BagTagId).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.DeviceId).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.Carousel).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.AirlineIATA).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.Flight).HasMaxLength(50).IsUnicode(false);
        builder.Property(x => x.JulianDate).HasMaxLength(10).IsUnicode(false);

        builder.HasIndex(x => x.BagTagId);
        builder.HasIndex(x => x.Date);
    }
}
