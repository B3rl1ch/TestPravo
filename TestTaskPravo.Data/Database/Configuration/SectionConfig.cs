using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Database.Configuration;

public class SectionConfig : IEntityTypeConfiguration<SectionDbo>
{
    public void Configure(EntityTypeBuilder<SectionDbo> e)
    {
        e.ToTable("Sections");
        e.HasKey(x => x.Id);
        
        e.Property(x => x.Title)
            .HasMaxLength(1024)
            .IsRequired();
    }
}