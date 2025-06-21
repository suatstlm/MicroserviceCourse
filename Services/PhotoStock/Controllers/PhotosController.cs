using Microsoft.AspNetCore.Mvc;
using PhotoStock.Dtos;
using Shared.Dtos;

namespace PhotoStock.Controllers
{
    public class PhotosController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotosController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo == null || photo.Length <= 0)
            {
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = Path.Combine("photos", photo.FileName);

            var photoDto = new PhotoDto { Url = returnPath };

            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
} 