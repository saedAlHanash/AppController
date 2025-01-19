namespace Data.Models;

public class ProductParam
{
    public int Id { get; set; }

    public int AndroidVersion { get; set; }

    public int IosVersion { get; set; }

    public bool IsIosTest { get; set; }

    public string AndroidUrl { get; set; }

    public string IosUrl { get; set; }

    public string AndroidDirectUrl { get; set; }
    
    public int ProductId { get; set; }

    public Product Product { get; set; }
    
}