using Microsoft.EntityFrameworkCore;
using SomeBlog.Domain.Entities;

namespace SomeBlog.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(c => c.Comments)
                .WithOne(e => e.Blog)
                .IsRequired();

            modelBuilder.Entity<Blog>()
                .HasMany(c => c.Categories)
                .WithMany(e => e.Blogs);
        }
    }
}
