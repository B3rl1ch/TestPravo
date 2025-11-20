using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Database.Configuration;

public class ArticleConfig : IEntityTypeConfiguration<ArticleDbo>
{
    public void Configure(EntityTypeBuilder<ArticleDbo> e)
    {
        e.ToTable("Articles");
        e.HasKey(x => x.Id);

        e.Property(x => x.Title)
            .HasMaxLength(256)
            .IsRequired();

        e.Property(x => x.CreatedAt).IsRequired();
        e.Property(x => x.UpdatedAt);
    }
}