namespace Data.Models;

public class FileRecord
{
    public int Id { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    public string ThumbFilePath { get; set; }

    public string MediumFilePath { get; set; }

    public long FileSize { get; set; }


    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}

public class FileImages
{
    public string Url { get; set; }

    public string UrlThumb { get; set; }

    public string UrlMedium { get; set; }
}