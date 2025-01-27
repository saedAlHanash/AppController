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
            .ForMember(dest => dest.Orgenal, opt => opt.MapFrom((record) => record.FileName.FixUrlFile()))
            .ForMember(dest => dest.Medium, opt => opt.MapFrom((record) => record.ThumbFilePath.FixUrlFile()))
            .ForMember(dest => dest.Thumpnil, opt => opt.MapFrom((record) => record.MediumFilePath.FixUrlFile()));
    }
}