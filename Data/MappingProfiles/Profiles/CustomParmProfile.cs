using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.MappingProfiles.Profiles;

public class CustomParmProfile : Profile
{
    public CustomParmProfile()
    {
        CreateMap<CustomParm, CustomParmDto>().ReverseMap();
    }
}