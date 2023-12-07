using AutoMapper;
using FamiliesAPI.Entities.DTOs;
using FamiliesAPI.Entities.Models;
using System.Globalization;

namespace FamiliesAPI.Services.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FamilyGroupModel, FamilyGroupDto>();
            CreateMap<FamilyGroupDto, FamilyGroupModel>();
            CreateMap<UserDTO, UserModel>()
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate.ToString("dd/MM/yyyy")))
                .ForMember(p => p.HashKey, i => i.Ignore());
            CreateMap<UserModel, UserDTO>()
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.Birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None)));
            CreateMap<LoggerModel, LoggerDTO>();
        }
    }
}
