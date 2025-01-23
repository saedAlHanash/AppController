namespace Data.DTOs;

public class CreateProduct
{
    public string Name { get; set; }
    
    public int FileId { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }
}