using CourseApp.Service.PhotoStock.Dtos;
using CourseApp.Shared.Dtos;
using CourseApp.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CourseApp.Service.PhotoStock.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class PhotosController :BaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo is not null and {Length :>0})
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Photos",photo.FileName);
            using var stream = new FileStream(path,FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);
            var returnPath = "photos/" + photo.FileName;

            PhotoDto photoDto = new()
            {
                Url = returnPath
            };
            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }
        return CreateActionResultInstance(Response<NoContent>.Fail("Resim Eklenemedi.", 400));
    }

    [HttpGet]
    public  IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Photos",photoUrl);
        if (!System.IO.File.Exists(path))
        {
            return CreateActionResultInstance(Response<NoContent>.Fail("Resim Silinemedi.", 400));
        }
        System.IO.File.Delete(path);
        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}