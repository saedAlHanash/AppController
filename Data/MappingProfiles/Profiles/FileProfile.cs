using AutoMapper;
using Data.DTOs;
using Data.Models;


namespace Data.MappingProfiles.Profiles;

public class FileProfile : Profile
{
    public FileProfile()
    {
        CreateMap<FileRecord, FileDto>()
            .ForMember(dest => dest.Orgenal, opt => opt.MapFrom((record) => record.FilePath.FixUrlFile()))
            .ForMember(dest => dest.Thumpnil, opt => opt.MapFrom((record) => record.ThumbFilePath.FixUrlFile()))
            .ForMember(dest => dest.Medium, opt => opt.MapFrom((record) => record.MediumFilePath.FixUrlFile()));
    }
}