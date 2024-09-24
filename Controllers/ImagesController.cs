using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBlog.Web.Repositories;
using System.Net;

namespace MusicBlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            try
            {
                // Dosya yükleme işlemini başlatıyoruz
                var imageURL = await imageRepository.UploadAsync(file);

                // Eğer yükleme sonucu null dönerse, hata mesajı döndürüyoruz
                if (imageURL == null)
                {
                    return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
                }

                // Başarılıysa JSON formatında URL'yi döndürüyoruz
                return new JsonResult(new { link = imageURL });
            }
            catch (Exception ex)
            {
                // Hata durumunda exception mesajını loglayıp, HTTP 500 hatası döndürüyoruz
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Problem("An error occurred while processing your request.", null, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}