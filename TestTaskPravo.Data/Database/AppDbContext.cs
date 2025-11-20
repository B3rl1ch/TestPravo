using Microsoft.EntityFrameworkCore;
using TestTaskPravo.Data.Database.Configuration;
using TestTaskPravo.Data.Models;

namespace TestTaskPravo.Data.Database;

public class AppDbContext : DbContext
{
    public DbSet<ArticleDbo> Articles => Set<ArticleDbo>();
    public DbSet<TagDbo> Tags => Set<TagDbo>();
    public DbSet<ArticleTagDbo> ArticleTags => Set<ArticleTagDbo>();
    public DbSet<SectionDbo> Sections => Set<SectionDbo>();
    public DbSet<SectionTagDbo> SectionTags => Set<SectionTagDbo>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}


    protected override void OnModelCreating(ModelBuilder b)
    {
        b.ApplyConfiguration(new ArticleConfig());
        b.ApplyConfiguration(new ArticleTagConfig());
        b.ApplyConfiguration(new SectionConfig());
        b.ApplyConfiguration(new SectionTagConfig());
        b.ApplyConfiguration(new TagConfig());
    }
}