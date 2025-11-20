using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Database.Configuration;

public class TagConfig : IEntityTypeConfiguration<TagDbo>
{
    public void Configure(EntityTypeBuilder<TagDbo> e)
    {
        e.ToTable("Tags");
        e.HasKey(x => x.Id);
        e.Property(x => x.Name)
            .HasMaxLength(256)
            .IsRequired();
        
        e.HasIndex(x => x.Name).IsUnique();
    }
}