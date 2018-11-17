using System.Linq;
using AutoMapper;
using recharge.Api.Dtos;
using recharge.Api.models;

namespace DattingApp.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<User, UserForListDto>()
            //     .ForMember(dest => dest.PhotoUrl, opt => {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain == true).Url);
            //     })
            //     .ForMember(dest => dest.Age, opt => {
            //         opt.ResolveUsing(src => src.DateOfBirth.Age());
            //     });

            // CreateMap<User, UserForDetailsDto>()
            //     .ForMember(dest => dest.PhotoUrl, opt => {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.isMain == true).Url);
            //     })
            //     .ForMember(dest => dest.Age, opt => {
            //         opt.ResolveUsing(src => src.DateOfBirth.Age());
            //     });

            // CreateMap<Photo, PhotoForDetailsDto>();

            // CreateMap<UserForUpdateDto, User>();

            // CreateMap<PhotoForCreationDto, Photo>();

            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto,User>();

            CreateMap<User,UserToReturnDto>();
            CreateMap<UserToReturnDto,User>();

            // CreateMap<Point, PointsToReturn>();
        }
    }
}