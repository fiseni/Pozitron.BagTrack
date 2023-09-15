using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PozitronDev.BagTrack.Domain.Messaging;

namespace PozitronDev.BagTrack.Infrastructure.Configurations;

public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.Property(x => x.Data).HasColumnType("nvarchar(max)");
    }
}
