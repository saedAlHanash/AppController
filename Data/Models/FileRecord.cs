namespace Data.Models;

public class FileRecord
{
    public int Id { get; set; } // Primary Key

    public string FileName { get; set; } // Original file name

    public string FilePath { get; set; }

    public string ThumbFilePath { get; set; }

    public string MediumFilePath { get; set; }

    public long FileSize { get; set; } // File size in bytes


    public DateTime UploadedAt { get; set; } = DateTime.UtcNow; // Upload timestamp
}