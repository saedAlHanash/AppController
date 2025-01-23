namespace Data.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int FileRecordId { get; set; }
    
    public FileRecord FileRecord { get; set; }
}