using System.Linq;
using AutoMapper;
using recharge.api.Controllers.HttpResource.HttpRequestResource.Payment;
using recharge.api.Controllers.HttpResource.HttpResponseResource;
using recharge.api.Dtos;
using recharge.api.Dtos.Payments;
using recharge.api.Core.Models;

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
            CreateMap<RegisterUserDto,User>()
                .ForMember(u => u.Referer, opt => opt.Ignore());

            //Data to return back to the user
            CreateMap<User,UserToReturnDto>();
            // CreateMap<UserToReturnDto,User>();

            //Point to send to the User
            CreateMap<Point, PointToReturnDto>();

            CreateMap<Card,CardDto>();

            // CreateMap<Point, PointsToReturn>();

            //------------- From Domain to Resource
            CreateMap<User, UserResponseResource>();
            CreateMap<Card, CardResponseResource>()
                .ForMember(c => c.CardNumber, opt => opt.MapFrom(card => card.CardNumber.Substring(card.CardNumber.Length - 4)));
            CreateMap<Point, PointResponseResource>();
            CreateMap<Card, CardRequestResource>();

            //------------- From Resource to Domain
            CreateMap<CardRequestResource, Card>();

            //------------- From Resource to Resource
            CreateMap<CardRequestFreeResource, CardRequestResource>();
        }
    }
}