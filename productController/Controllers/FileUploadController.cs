using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

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


        var uploadsFolder = Path.Combine(_config["ResourcesPath"], "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        // Save the file to the file system
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Create a database record for the file
        var fileRecord = new FileRecord
        {
            FileName = file.FileName,
            FilePath = filePath,
            FileSize = file.Length
        };

        _context.FileRecords.Add(fileRecord);
        await _context.SaveChangesAsync();

        // Return the uploaded file's information
        return Ok(new
        {
            fileRecord.Id,
            fileRecord.FileName,
            fileRecord.FileSize,
            UploadedAt = fileRecord.UploadedAt,
            Url = $"/uploads/{fileName}"
        });
    }
}