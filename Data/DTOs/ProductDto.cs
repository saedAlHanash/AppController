namespace Data.DTOs;

public class CreateProduct
{
    public string Name { get; set; }
    public int FileId { get; set; }

    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public string? AndroidUrl { get; set; }

    public string? IosUrl { get; set; }

    public string? AndroidDirectUrl { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public FileDto Images { get; set; }
    
    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public string? AndroidUrl { get; set; }

    public string? IosUrl { get; set; }

    public string? AndroidDirectUrl { get; set; }
    
}
public class ProductDtoForList
{
    public int Id { get; set; }

    public string Name { get; set; }

    public FileDto Images { get; set; }
    
    
}