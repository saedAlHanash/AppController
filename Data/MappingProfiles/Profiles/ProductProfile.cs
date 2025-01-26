using AutoMapper;
using Data.DTOs;
using Data.Models;


namespace Data.MappingProfiles.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FileRecord));


        CreateMap<FileRecord, FileImages>()
            .ForMember(
                dest => dest.Orgenal,
                opt => opt.MapFrom((record, images, arg3, context) =>
                {
                    return $"{context.Items["baseUrl"]}/{record.FileName}";
                }))
            .ForMember(dest => dest.Medium,
                opt => opt.MapFrom((record, images, arg3, context) =>
                {
                    return $"{context.Items["baseUrl"]}/{record.ThumbFilePath}";
                }))
            .ForMember(dest => dest.Thumpnil,
                opt => opt.MapFrom((record, images, arg3, context) =>
                {
                    return $"{context.Items["baseUrl"]}/{record.MediumFilePath}";
                }));
    }
}