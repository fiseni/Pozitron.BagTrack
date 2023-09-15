using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PozitronDev.BagTrack.Domain.Messaging;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class OutboxMessagesConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.Property(x => x.Data).HasColumnType("nvarchar(max)");
    }
}
