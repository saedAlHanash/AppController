using Data;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace productController.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FileUploadController(AppDbContext context, IConfiguration config) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] CreateFileDto input)
    {
        var file = input.filel;
        if (file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var fileName = Guid.NewGuid() + file.FileExtension();

        var filePath = Path.Combine(config.Uploads(), fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await CompressImage(file: file);


        var fileRecord = new FileRecord
        {
            FileName = file.FileName,
            FileSize = file.Length,
            FilePath = $"uploads/{fileName}",
            ThumbFilePath = $"thumbs/{fileName}",
            MediumFilePath = $"mediums/{fileName}",
        };

        var x = context.FileRecords.Add(fileRecord).Entity;

        await context.SaveChangesAsync();

        return Ok(new
            {
                fileRecord.Id,
                fileRecord.FileName,
                fileRecord.FileSize,
            }
        );
    }

    private async Task CompressImage(IFormFile file)
    {
        if (!file.FileName.IsImageByExtension()) return;


        using var image = await Image.LoadAsync(file.OpenReadStream());
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(600 * 2, 800 * 2),
            Mode = ResizeMode.Max,
        }));


        await image.SaveAsJpegAsync(path: config.Medium());

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(150, 150), // Thumbnail size
            Mode = ResizeMode.Crop
        }));

        await image.SaveAsJpegAsync(path: config.Thumb());
    }
}