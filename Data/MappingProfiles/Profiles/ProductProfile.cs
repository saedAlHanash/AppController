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

        CreateMap<Product, UpdateProduct>()
            .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => src.FileRecordId)).ReverseMap();

        CreateMap<Product, ProductDtoForList>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.FileRecord));
    }
}