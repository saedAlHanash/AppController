namespace Data.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public string? AndroidUrl { get; set; }

    public string? IosUrl { get; set; }

    public string? AndroidDirectUrl { get; set; }

    public int FileRecordId { get; set; }

    public FileRecord? FileRecord { get; set; }

    // 👇 هنا نضيف قائمة CustomParm
    public List<CustomParm> CustomParm { get; set; } = [];
}