namespace Data.Models;

public class CustomParm
{
    public int Id{ get; set; }

    public required string Key{ get; set; }

    public required string Value{ get; set; }

    public required int ProductId { get; set; }  
    
    public Product Product { get; set; }  
}