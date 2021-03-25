using AutoMapper;
using UsersTBC.Application.Models;
using UsersTBC.Domain.Entities;

namespace ProductTermsControl.WebAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRequestModel>();
            CreateMap<UserRequestModel, User>();
            CreateMap<UserResponseModel, User>();
            CreateMap<User, UserResponseModel>();


            CreateMap<City, CityModel>();
            CreateMap<CityModel, City>();

            CreateMap<UserImage, UserImageModel>();
            CreateMap<UserImageModel, UserImage>();

            CreateMap<UserMobileNumber, UserMobileNumberModel>();
            CreateMap<UserMobileNumberModel, UserMobileNumber>();
            

            CreateMap<UseRelated, UserRelatedModel>();
            CreateMap<UserRelatedModel, UseRelated>();

        }
    }
}