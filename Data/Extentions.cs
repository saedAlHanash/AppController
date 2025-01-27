using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Data;

public static class Extensions
{
    public static string FixUrlFile(this string str) =>
        string.IsNullOrWhiteSpace(str) ? str : $"{AppProvider.Instance.BaseUrl}/{str}";

    public static string FileExtension(this IFormFile file) => Path.GetExtension(file.FileName);


    public static bool IsImageByExtension(this string fileName)
    {
        string[] validExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".webp"];
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        return validExtensions.Contains(fileExtension);
    }

    public static string Uploads(this IConfiguration config) => Path.Combine(config["ResourcesPath"]!, "uploads");

    public static string Thumb(this IConfiguration config) => Path.Combine(config["ResourcesPath"]!, "thumbs");

    public static string Medium(this IConfiguration config) => Path.Combine(config["ResourcesPath"]!, "mediums");
}