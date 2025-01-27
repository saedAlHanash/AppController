using Microsoft.AspNetCore.Http;

namespace Data.DTOs;

public class FileDto
{
}public class CreateFileDto
{
        public IFormFile filel { set; get; }
}

public class FileImages
{
        public string Thumpnil { set; get; }
    
        public string Medium { set; get; }
    
        public string Orgenal { set; get; }
}