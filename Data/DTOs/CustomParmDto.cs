namespace Data.DTOs;

public class CustomParmDto
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }
    
    public int ProductId { get; set; }
}

public class CreateCustomParmDto
{

    public string Key { get; set; }

    public string Value { get; set; } 

    public int ProductId { get; set; }
}

public class UpdateCustomParmDto
{
    public int Id { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }
}