using Data;
using Data.Models;

namespace productController.Services;

public class FileService
{
    private readonly AppDbContext _context;

    public FileService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FileImages> GetImages(int id, string baseUrl)
    {
        var file = await _context.FileRecords.FindAsync(id);

        var fileUrl = $"{baseUrl}/{file.FileName}";
        var thumbUrl = $"{baseUrl}/{file.ThumbFilePath}";
        var mediumUrl = $"{baseUrl}/{file.MediumFilePath}";


        return new FileImages
        {
            Url = fileUrl,
            UrlThumb = thumbUrl,
            UrlMedium = mediumUrl,
        };
    }
}