using MusicBlog.Web.Models.Domain;

namespace MusicBlog.Web.Models.ViewModels
{
    public class HomeViewModel
    {

        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }


    }
}
