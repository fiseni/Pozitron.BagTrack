using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.Property(x => x.Id).HasMaxLength(10).IsUnicode(false);
        builder.Property(x => x.Carousel).HasMaxLength(10).IsUnicode(false);
    }
}
