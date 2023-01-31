using IteratesBrewManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IteratesBrewManager.Infrastructure.Persistence.Configurations;

public class BeerConfiguration : IEntityTypeConfiguration<Beer>
{
    public void Configure(EntityTypeBuilder<Beer> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}
