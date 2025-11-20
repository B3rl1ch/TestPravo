using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Database.Configuration;

public class ArticleTagConfig : IEntityTypeConfiguration<ArticleTagDbo>
{
    public void Configure(EntityTypeBuilder<ArticleTagDbo> e)
    {
        e.ToTable("ArticleTags");
        e.HasKey(x => new { x.ArticleId, x.TagId });

        e.HasOne(x => x.Article)
            .WithMany(a => a.Tags)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        e.HasOne(x => x.Tag)
            .WithMany()
            .HasForeignKey(x => x.TagId);
    }
}