using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.MappingProfiles.Profiles;

public class ProductParmProfile:Profile
{
    public ProductParmProfile()
    {
        CreateMap<ProductParam, ProductParamDto>();
    }
}