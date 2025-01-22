using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace productController.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    private readonly AppDbContext _context;


    private readonly IConfiguration _config;

    public FileUploadController(AppDbContext context, IConfiguration config)
    {
        _config = config;
        _context = context;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

        var directories = GetDirectories();

        var filePath = Path.Combine(directories[0], fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await CompressImage(file: file, fileName: fileName);


        var fileRecord = new FileRecord
        {
            FileName = file.FileName,
            FilePath = filePath,
            FileSize = file.Length,
            ThumbFilePath = $"thumb_{fileName}",
            MediumFilePath = $"medium_{fileName}",
        };

        _context.FileRecords.Add(fileRecord);
        
        await _context.SaveChangesAsync();
        
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var fileUrl = $"{baseUrl}/uploads/{fileName}";
        
        return Ok(new
            {
                fileRecord.Id,
                fileRecord.FileName,
                fileRecord.FileSize,
                Saed = "sdasf",
                Url = fileUrl
            }
        );
    }

    private async Task CompressImage(IFormFile file, string fileName)
    {
        if (IsImageByExtension(file.FileName)) return;
        var directories = GetDirectories();

        var thumbnailPath = Path.Combine(directories[1], $"thumb_{fileName}");
        var mediumPath = Path.Combine(directories[2], $"medium_{fileName}");

        using (var image = await Image.LoadAsync(file.OpenReadStream()))
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(600 * 2, 800 * 2),
                Mode = ResizeMode.Max,
            }));


            await image.SaveAsync(mediumPath);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(150, 150), // Thumbnail size
                Mode = ResizeMode.Crop
            }));

            await image.SaveAsync(thumbnailPath);
        }
    }

    private bool IsImageByExtension(string fileName)
    {
        string[] validExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp" };
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        return validExtensions.Contains(fileExtension);
    }

    private List<string> GetDirectories()
    {
        var uploadsFolder = Path.Combine(_config["ResourcesPath"], "uploads");
        var uploadsThumbFolder = Path.Combine(_config["ResourcesPath"], "thumbs");
        var uploadsMediumFolder = Path.Combine(_config["ResourcesPath"], "mediums");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        if (!Directory.Exists(uploadsThumbFolder))
        {
            Directory.CreateDirectory(uploadsThumbFolder);
        }

        if (!Directory.Exists(uploadsMediumFolder))
        {
            Directory.CreateDirectory(uploadsMediumFolder);
        }

        return
        [
            uploadsFolder,
            uploadsThumbFolder,
            uploadsMediumFolder
        ];
    }
}