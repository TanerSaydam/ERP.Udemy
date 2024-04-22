using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations;

internal sealed class RecipeDetailConfiguration : IEntityTypeConfiguration<RecipeDetail>
{
    public void Configure(EntityTypeBuilder<RecipeDetail> builder)
    {
        builder.Property(p => p.Quantity).HasColumnType("decimal(7,2)");
    }
}
