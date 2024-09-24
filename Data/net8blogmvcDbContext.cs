using Microsoft.EntityFrameworkCore;
using MusicBlog.Web.Models.Domain;

namespace MusicBlog.Web.Data
{
    public class MusicBlogDbContext : DbContext
    {
        public MusicBlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
