using Microsoft.AspNetCore.Http;

namespace Data.DTOs;

public class FileDto

{
    public required int Id { set; get; }
    
    public required string Thumpnil { set; get; }

    public required string Medium { set; get; }

    public required string Orgenal { set; get; }
}

public class CreateFileDto
{
    public IFormFile filel { set; get; }
}
