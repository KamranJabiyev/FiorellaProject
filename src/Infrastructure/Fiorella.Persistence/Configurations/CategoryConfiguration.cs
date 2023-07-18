using Fiorella.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiorella.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x=>x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x=> x.Description).IsRequired(false).HasMaxLength(500);
    }
}
