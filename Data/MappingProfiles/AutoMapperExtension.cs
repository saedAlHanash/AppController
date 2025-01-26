using Microsoft.Extensions.DependencyInjection;

namespace Data.MappingProfiles;

public class AutoMapperExtension
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperExtension));
    }
}