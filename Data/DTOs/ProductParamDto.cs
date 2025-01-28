using Data.Models;

namespace Data.DTOs;

public class ProductParamDto
{
    public int Id { get; set; }

    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public string? AndroidUrl { get; set; }

    public string? IosUrl { get; set; }

    public string? AndroidDirectUrl { get; set; }
    
    public required ProductDto Product { get; set; }
}

public class CreateProductParamDto
{
    public required int ProductId { get; set; }

    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public required string AndroidUrl { get; set; }

    public required string IosUrl { get; set; }

    public required string AndroidDirectUrl { get; set; }
}

public class UpdateProductParamDto
{
    public int Id { get; set; }

    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public string? AndroidUrl { get; set; }

    public string? IosUrl { get; set; }

    public string? AndroidDirectUrl { get; set; }

    public required int ProductId { get; set; }
}