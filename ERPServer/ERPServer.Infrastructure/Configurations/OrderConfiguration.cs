using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations;
internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(p => p.OrderNumber).HasColumnType("varchar(16)");
        builder.Property(p => p.Status)
            .HasConversion(status => status.Value, value => OrderStatusEnum.FromValue(value));
    }
}
