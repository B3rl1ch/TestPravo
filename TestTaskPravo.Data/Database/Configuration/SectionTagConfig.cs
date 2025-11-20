using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Database.Configuration;

public class SectionTagConfig : IEntityTypeConfiguration<SectionTagDbo>
{
    public void Configure(EntityTypeBuilder<SectionTagDbo> e)
    {
        e.ToTable("SectionTags");
        e.HasKey(x => new { x.SectionId, x.TagId });

        e.HasOne(x => x.Section)
            .WithMany(s => s.Tags)
            .HasForeignKey(x => x.SectionId)
            .OnDelete(DeleteBehavior.Cascade);

        e.HasOne(x => x.Tag)
            .WithMany()
            .HasForeignKey(x => x.TagId);
    }
}