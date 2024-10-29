using BlogAppWASM.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAppWASM.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : 
            base(options) { }

        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost { Id = 1, Title = "Introduction to Blazor", Content = "Lorum Ipsum Sit etc" },
                new BlogPost { Id = 2, Title = "State Management in Blazor", Content = "Lorum Ipsum Sit etc" },
                new BlogPost { Id = 3, Title = "Building a Blog with Blazor", Content = "Lorum Ipsum Sit etc" }
            );
        }
    }
}
