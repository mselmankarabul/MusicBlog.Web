using Microsoft.EntityFrameworkCore;
using MusicBlog.Web.Data;
using MusicBlog.Web.Models.Domain;

namespace MusicBlog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly MusicBlogDbContext musicBlogDbContext;

        public TagRepository(MusicBlogDbContext musicBlogDbContext)
        {
            this.musicBlogDbContext = musicBlogDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await musicBlogDbContext.Tags.AddAsync(tag);
            await musicBlogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await musicBlogDbContext.Tags.FindAsync(id);
            if (existingTag != null) { musicBlogDbContext.Tags.Remove(existingTag);
                await musicBlogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await musicBlogDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
           return musicBlogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await musicBlogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {

                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await musicBlogDbContext.SaveChangesAsync();
                return existingTag;

            }
            return null;
        }
    }
}
