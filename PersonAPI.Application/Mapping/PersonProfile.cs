
using AutoMapper;
using PersonAPI.Application.DTOs;
using PersonAPI.Domain.Entities;

namespace PersonAPI.Application.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Name.GivenName))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Name.Surname))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name.FullName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Value))
            .ForMember(dest => dest.BirthLocation, opt => opt.MapFrom(src => src.BirthLocation))
            .ForMember(dest => dest.DeathLocation, opt => opt.MapFrom(src => src.DeathLocation));

        CreateMap<PersonVersion, PersonVersionDto>()
            .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.Name.GivenName))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Name.Surname))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name.FullName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Value))
            .ForMember(dest => dest.BirthLocation, opt => opt.MapFrom(src => src.BirthLocation))
            .ForMember(dest => dest.DeathLocation, opt => opt.MapFrom(src => src.DeathLocation));

        CreateMap<PersonAPI.Domain.ValueObjects.Location, LocationDto>()
            .ForMember(dest => dest.FormattedLocation, opt => opt.MapFrom(src => src.FormattedLocation));
    }
}


