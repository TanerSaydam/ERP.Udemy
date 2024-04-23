using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations;
internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.Property(p => p.Type)
            .HasConversion(type => type.Value, value => InvoiceTypeEnum.FromValue(value));
    }
}
