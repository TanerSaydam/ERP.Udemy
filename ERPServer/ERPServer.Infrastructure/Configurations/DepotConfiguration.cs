using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations;
internal sealed class DepotConfiguration : IEntityTypeConfiguration<Depot>
{
    public void Configure(EntityTypeBuilder<Depot> builder)
    {
        builder.Property(p => p.Name).HasColumnType("varchar(50)");
    }
}
