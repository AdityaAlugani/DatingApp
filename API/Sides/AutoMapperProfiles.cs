
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Sides
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,specialuserDto>()
            .ForMember(destination=>destination.photoUrl,options=>options.MapFrom(source=>source.Photos.SingleOrDefault(photo=>photo.IsMain).Url))
            .ForMember(destination=>destination.Age,options=>options.MapFrom(source=>DateTimeExtensions.CalculateAge(source.DateOfBirth)));
            CreateMap<Photo,photoDto>();
            CreateMap<memberUpdateDto,AppUser>()
            .ForMember(destination=>destination.Introduction,options=>options.MapFrom(source=>source.Introduction))
            .ForMember(destination=>destination.Interests,options=>options.MapFrom(source=>source.Interests))
            .ForMember(destination=>destination.Country,options=>options.MapFrom(source=>source.Country))
            .ForMember(destination=>destination.LookingFor,options=>options.MapFrom(source=>source.LookingFor))
            .ForMember(destination=>destination.City,options=>options.MapFrom(source=>source.City));
        }   
    }
}