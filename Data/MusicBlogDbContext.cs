using Microsoft.EntityFrameworkCore;
using MusicBlog.Web.Models.Domain;

namespace MusicBlog.Web.Data
{
    public class MusicBlogDbContext : DbContext
    {
        public MusicBlogDbContext(DbContextOptions<MusicBlogDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
