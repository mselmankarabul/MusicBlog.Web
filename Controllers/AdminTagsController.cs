using Microsoft.AspNetCore.Mvc;
using MusicBlog.Web.Models.ViewModels;
using MusicBlog.Web.Data;
using MusicBlog.Web.Models.Domain;
using MusicBlog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace MusicBlog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public MusicBlogDbContext MusicBlogDbContext { get; }

        
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {

            //using dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            if (tag != null)
            {
                var editTagRqeuest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };

                return View(editTagRqeuest);


            }
            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {

            var tag = new Tag
            {

                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName,

            };

            var updatedTag = await tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            { //show success
                return RedirectToAction("List");
            }
            else
            { //show error}
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }

            [HttpPost]
            async Task<IActionResult> DeleteAsync(EditTagRequest editTagRequest)
            {
                var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
                if (deletedTag != null)
                {
                    //success notification
                    return RedirectToAction("List");
                }
                //Hata mesajı gelsin eğer yoksa
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }


        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
