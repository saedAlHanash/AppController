namespace Data.Models;

public class FileRecord
{
    public int Id { get; set; }

    public required  string FileName { get; set; }

    public required  string FilePath { get; set; }

    public required  string ThumbFilePath { get; set; }

    public required  string MediumFilePath { get; set; }

    public long FileSize { get; set; }


    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

