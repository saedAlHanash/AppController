using AutoMapper;
using Data;
using Data.DTOs;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace productController.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FileUploadController(AppDbContext context, IConfiguration config, IMapper mapper) : ControllerBase
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


        await CompressImage(file: file, fileName: fileName);

        await using (var stream = new FileStream(config.Uploads(fileName: fileName), FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }


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

        return Ok(mapper.Map<FileDto>(x));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteFile(int id)
    {
        var file = await context.FileRecords.FindAsync(id);
        if (file == null)
        {
            return NotFound();
        }

        context.FileRecords.Remove(file);
        await context.SaveChangesAsync();

        return Ok(mapper.Map<FileDto>(file));
    }

    private async Task CompressImage(IFormFile file, string fileName)
    {
        if (!file.FileName.IsImageByExtension()) return;


        using var image = await Image.LoadAsync(file.OpenReadStream());

        var imageSize = image.Size;

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(imageSize.Width / 2, imageSize.Height / 2),
            Mode = ResizeMode.Max,
        }));

        await image.SaveAsJpegAsync(path: config.Medium(fileName: fileName));

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(imageSize.Width / 6, imageSize.Height / 6),
            Mode = ResizeMode.Crop
        }));
        await image.SaveAsJpegAsync(path: config.Thumb(fileName: fileName));
    }

    [HttpGet]
    public async Task<ActionResult<List<FileDto>>> GetAll()
    {

        var result = await context.FileRecords.ToListAsync();
        return Ok(mapper.Map<List<FileDto>>(result));
    }
}