using Microsoft.EntityFrameworkCore;
using MusicBlog.Web.Data;
using MusicBlog.Web.Models.Domain;

namespace MusicBlog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly MusicBlogDbContext musicBlogDbContext;

        public BlogPostRepository(MusicBlogDbContext musicBlogDbContext)
        {
            this.musicBlogDbContext = musicBlogDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await musicBlogDbContext.AddAsync(blogPost);
            await musicBlogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await musicBlogDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null) 
            { 
                musicBlogDbContext.BlogPosts.Remove(existingBlog);
                await musicBlogDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await musicBlogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }


        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await musicBlogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await musicBlogDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await musicBlogDbContext.BlogPosts.Include(x => x.Tags).
               FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageURL = blogPost.FeaturedImageURL;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.Tags = blogPost.Tags;

                await musicBlogDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
